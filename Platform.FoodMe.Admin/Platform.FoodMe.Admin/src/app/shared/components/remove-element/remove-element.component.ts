import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute} from '@angular/router';
import { environment } from '@env/environment';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';
import { element } from 'protractor';

@Component({
  selector: 'app-remove-element',
  templateUrl: './remove-element.component.html',
  styleUrls: ['./remove-element.component.scss']
})
export class RemoveElementComponent implements OnInit  {
  private http: HttpClient;
  title? : string;
  currentRoute: string;
  @Input() id:string;
  element='';

 constructor(public dialogref: MatDialogRef<RemoveElementComponent>,private route: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data: any,http: HttpClient){
      this.http = http;
    }

  ngOnInit(): void {
    // this.currentRoute= this.route.snapshot._routerState.url;
    this.currentRoute = this.route.snapshot.url.join('/');

    console.log(this.currentRoute);
    const splitted = this.currentRoute.split('/', 4);
    const elements=splitted[2];
    this.element=elements.slice(0,elements.length-1);
  }


  save(){
      return this.http.delete(`${environment.API}/${this.element}/Remove${this.element}/${this.data.id}`);
  }
}
