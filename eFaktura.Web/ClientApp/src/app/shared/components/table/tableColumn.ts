export interface TableColumn {
  /** Obj property to get the data to display */
  key?: string;
  /** Obj property to get the data to export */
  field?: string;

  /** Column Title to display on the table head */
  title?: string;
  /** Column Title to display on exported file */
  header?: string;

  /** Column width for the column */
  colWidth?: string;
  /** Is the column sortable (default: true)*/
  sortable?: boolean;
  /** Css class(es) to add to table column */
  colCssClassName?: string;
  /** Css class(es) to add to column header */
  colHeadClassName?: string;
  /** Css class(es) to add to non empty table column */
  colCssClassNameNonEmpty?: string;
  /** Obj property's value to add to column as css class */
  colCssClassProp?: string;
  /** Is the column header vertical aligned */
  verticalHeader?: boolean;
  /** should the column be formated as date */
  dateFormatted?: boolean;
  /** Is the column searchable by text field*/
  searchable?: boolean;
  /** Is the column searchable by dropdown */
  searchableFilter?: boolean;
  /** Is the column searchable by date range */
  searchableDateRange?: boolean;

  statusColor?: boolean;

  /** List of items for searchableFilter DropDown */
  items?: any[];
  /** Is the column a link to another page */
  linkable?: boolean;

  /** link to trigger when linkeable column its clicked */
  link?: string;
  /** Is the column editable */
  editable?: boolean;

  /** Cut text to show only part of it initially, then all text on row selected */
  cutTextShowOnClick?: boolean;

  /** Number of charactes to display initially */
  cutTextTo?: number;
}
