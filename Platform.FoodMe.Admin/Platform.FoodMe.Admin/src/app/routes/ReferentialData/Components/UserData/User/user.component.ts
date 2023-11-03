import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { RemoveElementComponent } from '@shared/components/remove-element/remove-element.component';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';
import { CreateOrEditUserComponent } from './create-or-edit-user-model/create-or-edit-user-model.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class userComponent implements OnInit {
  displayedColumns = [ 'email', 'userName','phoneNumber','creationTime', 'macAddress','twoFactorEnabled', 'actions'];
  dataSource: MatTableDataSource<UserVM>;
  id='';
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  showDeleteModal=false;
  @Input() child : MatDialog;
  constructor(private Userservice: UserService,
    public dialog: MatDialog,private router: Router,private route: ActivatedRoute) {
  }
   Users: UserVM[] = [];

  ngOnInit() {
    this.getUsers();
  }



  editUser(User: UserVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateUser/${User.id}`)
    // this.router.navigate([navigateToEdit]);
  }


  createUser()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createUser");
    // this.router.navigate([navigateToCreate]);
  }

  viewUser(User: UserVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewUser/${User.id}`)
    // this.router.navigate([navigateToView]);

  }


  refresh(): void {
    this.Userservice.getAllUsers().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getUsers()
  {
    this.Userservice.getAllUsers().subscribe(res =>{
      this.Users = res;
      this.dataSource = new MatTableDataSource(this.Users);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteUser(User: UserVM){
    this.dialog.afterAllClosed.subscribe(()=> this.refresh());
    this.showDeleteModal=true;
    this.id=User.id;
     this.dialog.open(RemoveElementComponent, {
      data :{id:this.id}
  });

  }
  openDialog(User: UserVM) {
    this.id=User.id;
    console.log(this.id);
    this.dialog.open(RemoveElementComponent, {
      data: {
        id: this.id,
      },
    });
  }

}
