import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import * as XLSX from 'xlsx';
import {OrderActionReasonDto} from 'app/routes/ReferentialData/Models/OrderManagement/OrderActionReasonDto';
import {OrderActionReasonService} from 'app/routes/ReferentialData/Services/OrderManagement/OrderActionReason.service';
import {MatSelectChange} from "@angular/material/select";
import {FormControl, FormGroup} from "@angular/forms";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-order-action-reason',
  templateUrl: './order-action-reason.component.html',
  styleUrls: ['./order-action-reason.component.scss']
})
export class OrderActionReasonComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort; @ViewChild(MatPaginator)
  showfiltre = false;
  walletSelect: any;
  myControl: any;
  displayFn: any;
  filteredOptions: any;
  showInputFields: boolean;
  showInputFiltre: boolean;
  isChecked = 0;
  dialogRef: any;
  selectedOption: any;
  public isCollapsed = false;
  listOrderAction: any;
  page: number = 0;
  size: number = 5;

  formValueFiltre: FormGroup

  formValue = new FormGroup({
    reasonDescription: new FormControl(''),
    reasonAction: new FormControl(0),
    reasonStatus: new FormControl(0),
  })


  displayedColumns = ['reasonDescription', 'reasonAction', 'status', 'actions'];
  dataSource: MatTableDataSource<OrderActionReasonDto> = new MatTableDataSource<OrderActionReasonDto>;

  constructor(private router: Router, private route: ActivatedRoute, public orderActionService: OrderActionReasonService, public dialog: MatDialog) {
  }


  ngOnInit(): void {
    this.loadData()
    this.initForm()
  }

  initForm() {
    this.formValueFiltre = new FormGroup({
      reasonDescription1: new FormControl(''),
      reasonAction1: new FormControl(0),
      reasonStatus1: new FormControl(0),
    })
  }

  loadData() {
    this.orderActionService.getOrderAction(this.page, this.size).subscribe(
      (data: any) => {
        if (data) {
          this.listOrderAction = data;
          this.dataSource = new MatTableDataSource(this.listOrderAction)
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }
    )
  }

  createOrderAction() {
 //   let navigateToCreate = this.route.snapshot._routerState.url.concat("/create-or-edit-order-action-reason");
  //  this.router.navigate([navigateToCreate,{page: "create"}]);
  }


  filtre() {
    this.orderActionService.getActionReason(this.formValueFiltre.value.reasonDescription1, this.formValueFiltre.value.reasonAction1, this.formValueFiltre.value.reasonStatus1 == false ? 0 : 1, this.page, this.size).subscribe(
      (data: any) => {
        if (data) {
          this.listOrderAction = data;
          this.dataSource = new MatTableDataSource(this.listOrderAction)
          console.log('dataload')
        }
      }
    )
  }

  deleteOrderAction(id: any) {
    if (confirm("êtes-vous sur de supprimer OrderAction ?")) {
      this.orderActionService.deleteOrderAction(id)
        .subscribe(_res => {
            alert("OrderAction supprimé ");
            this.loadData();
          }
        )
    }

  }


  changeAttributeValue() {
    this.isChecked == 0 ? this.isChecked = 1 : this.isChecked = 0
  }

  exportExcel(): void {

    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listOrderAction);
    const workbook: XLSX.WorkBook = {Sheets: {'listOrderAction': worksheet}, SheetNames: ['listOrderAction']};
    const excelBuffer: any = XLSX.write(workbook, {bookType: 'xlsx', type: 'array'});
    this.saveAsExcelFile(excelBuffer, 'listOrderAction');
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {"type": 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  editOrderAction(OrderActionReasonDto: OrderActionReasonDto) {
    //let navigateToEdit = this.route.snapshot._routerState.url.concat("/create-or-edit-order-action-reason");
 //   this.router.navigate([navigateToEdit, {page :"edit", my_object: JSON.stringify(OrderActionReasonDto) }]);

  }

  viewOrderAction(OrderActionReasonDto: OrderActionReasonDto) {
   // let navigateToView = this.route.snapshot._routerState.url.concat("/create-or-edit-order-action-reason");
   // this.router.navigate([navigateToView, {page :"view", my_object: JSON.stringify(OrderActionReasonDto) }]);
  }

  onSelectionChange($event: MatSelectChange) {
    let listOrderActionFiltred: any = [];
    this.listOrderAction.forEach((orderAction: OrderActionReasonDto) => {
      if (orderAction.status == $event.value) {
        listOrderActionFiltred.push(orderAction)
      }
    })
    this.dataSource = new MatTableDataSource(listOrderActionFiltred)
  }

  ShowFiltre() {
    this.showfiltre = true;

  }
  pageChanged($event: PageEvent) {
    this.loadDataPaginated($event.pageIndex,$event.pageSize);
    this.paginator.pageSize=$event.pageSize;
    this.paginator.pageIndex=$event.pageIndex;


  }
  loadDataPaginated(page:number,size:number) {

    this.orderActionService.getOrderAction(page,size).subscribe(
      data => {
        this.listOrderAction = data;
        this.dataSource = new MatTableDataSource(this.listOrderAction);
      }
    );
  }
}

