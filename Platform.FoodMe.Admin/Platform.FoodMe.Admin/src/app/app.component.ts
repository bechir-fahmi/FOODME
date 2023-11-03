import { Component, OnInit, AfterViewInit, NgZone } from '@angular/core';
import { PreloaderService } from '@core';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit, AfterViewInit {

  map: any;
  drawingManager: any;
  constructor(private preloader: PreloaderService,private zone: NgZone) {}

  ngOnInit() {}

  ngAfterViewInit() {
    this.preloader.hide();
  
  }



}
