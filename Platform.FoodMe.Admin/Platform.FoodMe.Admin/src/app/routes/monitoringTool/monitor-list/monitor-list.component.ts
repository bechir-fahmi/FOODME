import { Component, OnInit } from '@angular/core';
import { OrderFilterVM } from 'app/routes/ReferentialData/Models/OrderManagement/OrderFilterVM';
import { MonitorService } from 'app/routes/ReferentialData/Services/monitoringToolsData/monitor.service';

@Component({
  selector: 'app-monitor-list',
  templateUrl: './monitor-list.component.html',
  styleUrls: ['./monitor-list.component.scss']
})
export class MonitorListComponent implements OnInit{
numberCol :number = 0; 
listMonitor:OrderFilterVM[] = [];
constructor( private monitorService :MonitorService){
  
}
ngOnInit(): void {
// this.numberCol =  this.listMonitor.length/2
this.refresh();
}

delete(id: number){
  this.monitorService.deleteOrderFilter(id).subscribe(
    () => {
     this.refresh();
    });
  }

  refresh(){
    this.monitorService.getAllOrderFiltre().subscribe(res=>{
      this.listMonitor = res; 
    });
  }

}


