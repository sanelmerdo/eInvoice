import { Component, OnInit, Input, ViewChild, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { TableColumn } from './tableColumn';
import { Table } from 'primeng/table';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  providers: [DatePipe]
})
export class TableComponent implements OnInit {

  /** Key that identifies the rows as unique */
  @Input() dataKey: string = "";

  /** Array of columns to display on the table */
  @Input() columns: TableColumn[] = [];

  /** Array of data to display on the table */
  @Input() set data(data: any[]) {
    this.fullData = data;
    this.filteredData = data;
    this.dt.reset();

    if (this.showingAll) {
      this.rowsPerPage = data.length;
    }

    if (this.filters.length > 0) {
      this.filterByColumn();
    }

  }
  filteredData: any[] = [];
  fullData: any[] = [];

  /** Should the column header filter be shown */
  @Input() filterableTable: boolean;

  /** Global search term to filter by */
  @Input() searchTerm: string = "";

  /** Export file name */
  @Input() exportFileName: string = "TI_Table.csv";
  /** Event to export data to excel */
  @Input() exportToExcel: boolean;

  /** Event to clear table filters */
  @Input() refreshButtonClicked: boolean;

  /** Should edit buttons be shown (enable editing) */
  @Input() showEditColumn: boolean = false;

  /** Should delete button be shown (enable deleting)*/
  @Input() showDeleteColumn: boolean = false;

  /** Enforce column with to be the expressed on the column obj, otherwise auto */
  @Input() enforceColWidth: boolean = false;

  /** Event when the user clicks on the edit button, returns bind row object */
  @Output() RowEdit = new EventEmitter<any>();

  /*** Event when the user clicks on the delete button, returns bind row object */
  @Output() RowDelete = new EventEmitter<any>();

  @Input() pageSize: number = 20;
  /** TODO: fix but when this field its availble as Input() */
  rowsPerPage: number = 20;

  @Input() dateFormat: string = "MM-dd-yyyy";
  sortComparator = new Intl.Collator("en", { numeric: true, sensitivity: 'base' });

  @ViewChild(Table, { read: false, static: true }) dt: Table;

  @Input() filters: any = [];
  cutTextTo: number = 150;
  selectedRow: any = {};
  showingAll = false;
  showEditableRow = true;

  dateFilterToolTip = "Format:<br>{=,>,<}MM-dd-yyyy & ...";

  constructor(private datePipe: DatePipe) { }

  ngOnInit() { }


  ngOnChanges(changes: SimpleChanges): void {
    if (!changes) return;

    if (changes.pageSize) {
      this.rowsPerPage = this.pageSize;
    }

    if (changes.data && !changes.data.firstChange) {
      this.showingAll = changes.data.currentValue.length == this.rowsPerPage;
      this.filterByColumn();
    }

    if (changes.searchTerm && !changes.searchTerm.firstChange) {
      this.filterByColumn();
    }

    if (changes.refreshButtonClicked && !changes.refreshButtonClicked.firstChange) {
      this.filters = [];
      this.filterByColumn();
    }

    else if (changes.exportToExcel && this.fullData.length) {

      // set excel column head if not set
      this.columns.forEach(c => {
        if (!c.field) c.field = c.key;
        if (!c.header) c.header = c.title;
      });

      this.dt.exportFilename = this.exportFileName;
      this.dt.exportCSV();
    }
    if (changes.filters) {
      this.filterByColumn();
    }
  }

  onRowEditSave(item) {
    this.RowEdit.emit(item);
  }

  /**
   * Get the list of columns to display  on the table
   */
  getColumns() {
    console.log(this.columns.map(col => col.key));
    return this.columns.map(col => col.key);
  }

  /**
   * Get the css classes to add to table column
   * @param row Current data row
   * @param col Current col
   */
  getColClasses(row, col) {
    let classes = col.colCssClassName || "tbl-col ";

    if (col.colCssClassProp && !col.linkable) {
      classes += " " + col.colCssClassProp + "-" + row[col.colCssClassProp];
    }
    if (col.verticalHeader && row[col] != null) {
      classes += " green-cell";
    }
    return classes;
  }

  setItemForKey(row, col, value, byLabel = false) {
    let item = row[col.key];
    let dropDownSelectedObject = null;

    // if the colm is drop
    if (col.items) {
      dropDownSelectedObject = col.items.find(i => (!byLabel ? i.value == value : i.label == value));
    }

    if (col.key.includes(".")) {
      // the key is to a complex object
      let levels = col.key.split(".");
      item = row[levels[0]];
      for (let i = 1; i < levels.length - 1; i++) {
        item = item[levels[i]];
      }

      var lastKey = levels[levels.length - 1];
      if (!col.items) {
        // set the text to input field
        item[lastKey] = value;
      }
      else {
        // set the value and selected object to dropdow
        var text = "";
        if (byLabel) {
          text = dropDownSelectedObject && dropDownSelectedObject.label || "";
        }
        else {
          text = dropDownSelectedObject && dropDownSelectedObject.value || "";
        }

        item[lastKey] = text;
        item._selectedItem = dropDownSelectedObject;
      }
    }
    else {
      // set value to a first level key
      // todo: add to row the selected item like _{key}selectedItem
      row[col.key] = value;
    }
  }

  getItemForKey(row, key) {
    let item = row[key];

    //if (key.includes(".")) {
    //  // the key is to a complex object
    //  let levels = key.split(".");
    //  item = row[levels[0]];
    //  for (let i = 1; i < levels.length; i++) {
    //    item = item[levels[i]];
    //  }
    //}

    return item;
  }

  getTextToDisplay(row, col) {
    let item = this.getItemForKey(row, col.key);

    if (!item) return "";
    if (col.editable && col.link) return "";

    if (col.searchableDateRange) {
      return this.datePipe.transform(item, this.dateFormat);
    }
    else {
      if (!this.selectedRow) return item;
      let isRowSelected = row[this.dataKey] == this.selectedRow[this.dataKey];
      if (!col.cutTextShowOnClick || isRowSelected) return item;

      // show only part of the text
      return ((item && item.substring(0, col.cutTextTo || this.cutTextTo)) || "");
    }
  }

  getRouterLink(row, col) {
    let colVal = !col.colCssClassProp.includes(".") ? row[col.colCssClassProp] : this.getItemForKey(row, col.colCssClassProp);
    return col.link.concat(colVal);
  }

  textAndDateComparator = (event: any) => {
    event.data.sort((data1, data2) => {
      let item1: any;
      let item2: any;

      if (!event.field.includes(".")) {
        item1 = data1[event.field];
        item2 = data2[event.field];
      }
      else {
        item1 = this.getItemForKey(data1, event.field);
        item2 = this.getItemForKey(data2, event.field);
      }

      let result = null;

      if (item1 == null && item2 != null)
        result = -1;
      else if (item1 != null && item2 == null)
        result = 1;
      else if (item1 == null && item2 == null)
        result = 0;
      else if (this.columns.filter(c => c.key == event.field)[0].searchableDateRange)
        result = new Date(item1) > new Date(item2) ? 1 : -1
      else
        result = this.sortComparator.compare(item1, item2);

      return (event.order * result);
    });
  }

  filterByColumn() {
    // reset pagination when filtering    
    this.dt.resetPageOnSort = true;

    this.filteredData = this.fullData.filter(row => {
      return (
        // Apply filter of global search box
        Object.keys(row).some(property => {
          if (row[property]) {
            return row[property]
              .toString()
              .toLowerCase()
              .includes(this.searchTerm.toLowerCase());
          }
        }) &&
        // apply filters of columns
        Object.keys(this.filters).every(key => {
          if (this.filters[key] === "" || this.filters[key] === null || !this.filters[key].length) {
            return true;
          }

          // get column text for the row
          let colText;
          if (key.includes(".")) colText = this.getItemForKey(row, key);
          else if (row[key]) colText = row[key].toString();

          if (colText) {

            let filterElement = this.filters[key];

            // filter is an array of strings
            if (Array.isArray(filterElement)) {
              if (!key.includes(".")) {
                return filterElement.includes(colText);
              }
              else {
                var found = false;
                filterElement.forEach(element => {
                  if (!found) {
                    let items = this.columns.filter(col => col.key == key)[0].items.filter(item => item.value == element);
                    found = items.length && items[0].label == colText;
                  }
                });
                return found;
              }
            }

            // apply date filter
            //else if (this.columns.filter(c => c.key == key)[0].searchableDateRange) {
            //  return DateFilter.matchesFilter(colText, filterElement);
            //}

            // filter is single string
            else {
              return colText.toLowerCase().includes(filterElement.toLowerCase());
            }
          }

          return false;
        })
      );
    });
  }

  pageChanged(event) {
    this.showingAll = event.rows == this.filteredData.length;
  }

  onRowDelete(item: any) {
    this.RowDelete.emit(item);
  }

  onRowEditStarts(row) {
    // store the original values to reset if edit canceled
    row._orgValues = {};
    this.showEditableRow = false;
    // look for the dropdown columns 
    // and change the text for the value
    this.columns.forEach(col => {
      if (col.items && col.key.includes(".")) {

        //set default empty value for dropdowns
        let hasEmpty = col.items.filter(x => x.label == "");
        if (hasEmpty == null || hasEmpty.length == 0) {
          col.items.unshift({ label: '', value: null });
        }
        // get the text currently displayed
        let label = this.getItemForKey(row, col.key);
        // get the value for the label
        let dropDownItem = col.items.find(i => i.label == label);
        // set the value instead of the column
        if (dropDownItem && dropDownItem.value) {
          this.setItemForKey(row, col, dropDownItem.value);
          row._orgValues[col.key] = dropDownItem.label;
        }
        else {
          row._orgValues[col.key] = "";
        }
      }
      else {
        row._orgValues[col.key] = row[col.key];
      }
    });
  }

  onRowEditEnds(row, editCanceled = false) {
    this.showEditableRow = true;
    // look for the dropdown columns 
    // and change the value for the text
    this.columns.forEach(col => {
      if (col.items && col.key.includes(".")) {
        let value = "";

        if (editCanceled) {
          value = row._orgValues[col.key];
        }
        else {
          // get the text currently displayed
          value = this.getItemForKey(row, col.key);

          // get the value for the label
          let dropDownItem = col.items.find(i => i.value == value);
          // set the value instead of the column
          if (dropDownItem && dropDownItem.label) value = dropDownItem.label;
        }

        this.setItemForKey(row, col, value, true);

      }
      else if (editCanceled) {
        row[col.key] = row._orgValues[col.key];
      }
    });
  }

}
