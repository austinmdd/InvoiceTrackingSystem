import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-busy',
  templateUrl: './busy.component.html',
  styleUrls: ['./busy.component.css']
})
export class BusyComponent implements OnInit {
    @Input() message: string = 'Please wait...'; 
  constructor() { }

  ngOnInit() {
  }

}
