import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RegistrationModel } from 'src/app/Entities/RegistrationModel';
import { IdentetyService } from 'src/app/Services/IdentetyService';

@Component({
  selector: 'app-regisration',
  templateUrl: './regisration.component.html',
  styleUrls: ['./regisration.component.css']
})
export class RegisrationComponent implements OnInit {

  public model: RegistrationModel  = new RegistrationModel();
  public roleForm: FormGroup;
  private _email: string = "";
  public get email(): string {
    return this._email;
  }
  public set useremail(value: string) {
    console.log("Set" + value);
    this._email = value;
  }

  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
    
  constructor(private fb: FormBuilder,private _service:IdentetyService ) { 
    this.roleForm = this.fb.group({
      toppings: [null]
    })
    
  }

  public Registration(){
    this._service.Registration(this.model)
  }

  matcher = new MyErrorStateMatcher();
  ngOnInit(): void {
  }
}
 
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
 
 
 