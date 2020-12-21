import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Client } from '../../../models/client';
import { TableColumn } from '../table/tableColumn';

@Component({
  selector: 'app-table2',
  templateUrl: './table2.component.html',
  styleUrls: ['./table2.component.scss']
})
export class Table2Component {

  @Input() cols: TableColumn;
  @Input() values: any;
  selectedValue: any;
  @Output() selectedDataEvent = new EventEmitter<any>();
  @Output() selectedClient = new EventEmitter<any>();

  onRowSelect(event) {
    this.selectedDataEvent.emit(this.selectedValue);
  }

  sendValues(event) {
    this.selectedClient.emit(event);
  }
}
