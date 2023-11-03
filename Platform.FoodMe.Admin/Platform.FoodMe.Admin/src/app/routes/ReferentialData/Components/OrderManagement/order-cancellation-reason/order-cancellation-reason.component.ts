import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { OrderCancellationResonService } from '../../../Services/OrderManagement/order-cancellation-reson.service';
import { OrderCancellationReasonDto } from '../../../Models/OrderManagement/OrderCancellationReasonDto';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';
import { EditCancellationReasonComponent } from './edit-cancellation-reason/edit-cancellation-reason.component';
import * as XLSX from 'xlsx';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-order-cancellation-reason',
  templateUrl: './order-cancellation-reason.component.html',
  styleUrls: ['./order-cancellation-reason.component.scss'],
})
export class OrderCancellationReasonComponent {
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort: MatSort = new MatSort;

  alletSelect: any;
  myControl: any;
  displayFn: any;
  filteredOptions: any;
  showInputFields: boolean;
  showInputFiltre: boolean;
  dialogRef: any;
  page: number = 0;
  size: number = 5;
  formValue = new FormGroup({
    reaSDESCRIPTION: new FormControl(''),
    descriptionEnName: new FormControl(''),
    reaSCONCEPTID: new FormControl(0),
    reaSID: new FormControl(0),
  });
  public isCollapsed = false;
  listOrderCancellationReson: any;
  displayedColumns = ['reaS_DESCRIPTION', 'reaS_CONCEPTID', 'reaS_ID', 'actions'];
  dataSource: MatTableDataSource<OrderCancellationReasonDto> =
    new MatTableDataSource<OrderCancellationReasonDto>();

  constructor(
    private router: Router,
    public orderCancellationResonService: OrderCancellationResonService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    this.orderCancellationResonService
      .getOrderCancellation(this.page, this.size)
      .subscribe((data: any) => {
        if (data) {
          this.listOrderCancellationReson = data;
          this.dataSource = new MatTableDataSource(this.listOrderCancellationReson);
        }
      });
  }
  pageChanged($event: PageEvent) {
    this.loadDataPaginated($event.pageIndex, $event.pageSize);
    this.paginator.pageSize = $event.pageSize;
    this.paginator.pageIndex = $event.pageIndex;
  }
  loadDataPaginated(page: number, size: number) {
    this.orderCancellationResonService.getOrderCancellation(page, size).subscribe(data => {
      this.listOrderCancellationReson = data;
      this.dataSource = new MatTableDataSource(this.listOrderCancellationReson);
    });
  }
  createCancellationReson() {
    this.router.navigate([
      'order-management/order-cancellation-reason/edit-cancellation-reason',
      { page: 'create' },
    ]);
  }
  editCancellationReson(OrderCancellationReasonDto: OrderCancellationReasonDto) {
    this.router.navigate([
      'order-management/order-cancellation-reason/edit-cancellation-reason',
      { page: 'edit', my_object: JSON.stringify(OrderCancellationReasonDto) },
    ]);
  }

  viewCancellationReson(OrderCancellationReasonDto: OrderCancellationReasonDto) {
    this.router.navigate([
      'order-management/order-cancellation-reason/edit-cancellation-reason',
      { page: 'view', my_object: JSON.stringify(OrderCancellationReasonDto) },
    ]);
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteorderCancellationReson(id: any) {
    if (confirm('êtes-vous sur de supprimer OrderCancellation ?')) {
      this.orderCancellationResonService.deleteOrderCancellation(id).subscribe(_res => {
        alert('OrderAction supprimé ');
        this.loadData();
      });
    }
  }
  exportExcel(): void {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listOrderCancellationReson);
    const workbook: XLSX.WorkBook = {
      Sheets: { OrderCancellationReson: worksheet },
      SheetNames: ['OrderCancellationReson'],
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, 'OrderCancellationReson');
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

  showUpdateButton() {
    this.dialogRef.componentInstance.showUpdateButton();
  }
}
