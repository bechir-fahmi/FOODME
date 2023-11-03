import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, AbstractControl, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public phone: string;
  fileToUpload : File=null  as any;
  imageUrl:string ="";

  myForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  registerForm = this.fb.nonNullable.group(
    {
      CampanyName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      Email: ['', [Validators.required]],
      PhoneNumber: ['', [Validators.required]],

      Location: ['', [Validators.required]],
      WebSite: ['', [Validators.required]],

      Picture: ['', [Validators.required]],


    },
    {
      validators: [this.matchValidator('password', 'confirmPassword')],
    }
  );

  constructor(private fb: FormBuilder ) {this.phone = '';}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  get f(){
    return this.myForm.controls;
  }

  onselectFile(e:any){
    if (e.target.files){
      var reader= new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload=(event:any)=>{
           this.imageUrl=event.target.result;}
    }


    // e.target.files ;
    // this.fileToUpload=file.item(0) as File;
    // var reader= new FileReader();
    // reader.onload=(event:any)=>{
    //   this.imageUrl=event.target.result;

    // }
    //

  }
  url = 'https://img.icons8.com/ios/100/000000/contract-job.png';
  onSelect(event:any) {
    let fileType = event.target.files[0].type;
    if (fileType.match(/image\/*/)) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event: any) => {
        this.url = event.target.result;
      };
    } else {
      window.alert('Please select correct image format');
    }
  }

  onFileChange(event:any) {
    const reader = new FileReader();

    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {

        this.imageUrl = reader.result as string;

        this.myForm.patchValue({
          fileSource: reader.result as string
        });

      };

    }
  }

  matchValidator(source: string, target: string) {
    return (control: AbstractControl) => {
      const sourceControl = control.get(source)!;
      const targetControl = control.get(target)!;
      if (targetControl.errors && !targetControl.errors.mismatch) {
        return null;
      }
      if (sourceControl.value !== targetControl.value) {
        targetControl.setErrors({ mismatch: true });
        return { mismatch: true };
      } else {
        targetControl.setErrors(null);
        return null;
      }
    };
  }


  createCompany(){

  }
}
