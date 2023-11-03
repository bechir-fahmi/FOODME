import { Component,Input } from '@angular/core';


@Component({
  selector: 'app-afco-ticket-order',
  templateUrl: './afco-ticket-order.component.html',
  styleUrls: ['./afco-ticket-order.component.scss']
})
export class AfcoTicketOrderComponent {

  @Input() order:any;

}
