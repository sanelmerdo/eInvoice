import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-auto-complete-dropdown',
  templateUrl: './auto-complete-dropdown.component.html',
  styleUrls: ['./auto-complete-dropdown.component.scss']
})
export class AutoCompleteDropdownComponent implements OnInit {

  text: string;

  results: string[];

  ngOnInit() {
  }

  search(event) {
  }

}
