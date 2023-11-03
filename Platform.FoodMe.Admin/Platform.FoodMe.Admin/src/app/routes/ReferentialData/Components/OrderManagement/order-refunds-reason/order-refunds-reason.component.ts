import { Component, ViewChild } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { MatTableDataSource } from '@angular/material/table';
import { OrderActionReasonDto } from '../../../Models/OrderManagement/OrderActionReasonDto';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { OrderRefundsReasonDto } from '../../../Models/OrderManagement/OrderRefundsReasonDto';
import { OrderRefundsReasonService } from '../../../Services/OrderRefundsReasonService/orderRefundsReason.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { EditOrderRefundComponent } from './edit-order-refund/edit-order-refund.component';
import * as XLSX from 'xlsx';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-order-refunds-reason',
  templateUrl: './order-refunds-reason.component.html',
  styleUrls: ['./order-refunds-reason.component.scss'],
})
export class OrderRefundsReasonComponent {
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort: MatSort = new MatSort();
  walletSelect: any;
  myControl: any;
  displayFn: any;
  selectedOption: any;
  filteredOptions: any;
  showInputFields: boolean;
  showInputFiltre: boolean;
  isChecked = 0;
  dialogRef: any;
  public isCollapsed = false;
  listOrderRefunds: any;
  page: number = 0;
  size: number = 5;
  formValueFiltre: FormGroup;
  formValue = new FormGroup({
    refundsDescription: new FormControl(''),
    reasonStatus: new FormControl(0),
  });
  displayedColumns = ['refundsDescription', 'status', 'actions'];
  dataSource: MatTableDataSource<OrderRefundsReasonDto> =
    new MatTableDataSource<OrderRefundsReasonDto>();

  constructor(
    private router: Router,
    public orderRefundsReasonService: OrderRefundsReasonService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.initForm();
  }
  initForm() {
    this.formValueFiltre = new FormGroup({
      RefundsDescription: new FormControl(''),
      reasonStatus1: new FormControl(0),
    });
  }
  loadData() {
    this.orderRefundsReasonService
      .getorderRefundsReason(this.page, this.size)
      .subscribe((data: any) => {
        if (data) {
          this.listOrderRefunds = data;
          this.dataSource = new MatTableDataSource(this.listOrderRefunds);
        }
      });
  }
  pageChanged($event: PageEvent) {
    this.loadDataPaginated($event.pageIndex, $event.pageSize);
    this.paginator.pageSize = $event.pageSize;
    this.paginator.pageIndex = $event.pageIndex;
  }
  loadDataPaginated(page: number, size: number) {
    this.orderRefundsReasonService.getorderRefundsReason(page, size).subscribe(data => {
      this.listOrderRefunds = data;
      this.dataSource = new MatTableDataSource(this.listOrderRefunds);
    });
  }
  createOrderRefundsReason() {
    this.router.navigate([
      'order-management/order-refunds-reason/edit-order-refund',
      { page: 'create' },
    ]);
  }
  editOrderRefunds(OrderRefundsReasonDto: OrderRefundsReasonDto) {
    this.router.navigate([
      'order-management/order-refunds-reason/edit-order-refund',
      { page: 'edit', my_object: JSON.stringify(OrderRefundsReasonDto) },
    ]);
  }

  viewOrderRefunds(OrderRefundsReasonDto: OrderRefundsReasonDto) {
    this.router.navigate([
      'order-management/order-refunds-reason/edit-order-refund',
      { page: 'view', my_object: JSON.stringify(OrderRefundsReasonDto) },
    ]);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  changeAttributeValue() {
    this.isChecked == 0 ? (this.isChecked = 1) : (this.isChecked = 0);
  }

  exportExcel() {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listOrderRefunds);
    const workbook: XLSX.WorkBook = {
      Sheets: { listOrderRefunds: worksheet },
      SheetNames: ['listOrderRefunds'],
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, 'listOrderRefunds');
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    });
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }

  deleteOrderRefunds(id: any) {
    if (confirm('êtes-vous sur de supprimerOrderRefunds ?')) {
      this.orderRefundsReasonService.deleteOrderRefundsReason(id).subscribe(_res => {
        alert('OrderAction supprimé ');
        this.loadData();
      });
    }
  }
  showUpdateButton() {
    this.dialogRef.componentInstance.showUpdateButton();
  }

  filtre() {
    console.log('bonour');
    this.orderRefundsReasonService
      .getRefundsReason(
        this.formValueFiltre.value.RefundsDescription,
        this.formValueFiltre.value.reasonStatus1 == false ? 0 : 1,
        this.page,
        this.size
      )
      .subscribe((data: any) => {
        if (data) {
          this.listOrderRefunds = data;
          this.dataSource = new MatTableDataSource(this.listOrderRefunds);
          console.log('dataload');
        }
      });
  }
}
