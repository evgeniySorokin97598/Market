import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RegistrationModel } from 'src/app/Entities/RegistrationModel';
import { IdentetyService } from 'src/app/Services/IdentetyService';
import { ModalDismissReasons, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-regisration',
  templateUrl: './regisration.component.html',
  styleUrls: ['./regisration.component.css'],
})
export class RegisrationComponent implements OnInit {
  

 
  @Output() onSuggest: EventEmitter<any> = new EventEmitter();
 
 
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
  closeResult = '';
    
  constructor(private fb: FormBuilder,private _service:IdentetyService ,private modalService: NgbModal) { 
    this.roleForm = this.fb.group({
      toppings: [null]
    })
    
  }

  public async Registration(){
    await this._service.Registration(this.model)
    console.log("вызов колбэка");
    this.onSuggest.emit();
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
 
  private getDismissReason(reason: any): string {
		if (reason === ModalDismissReasons.ESC) {
			return 'by pressing ESC';
		} else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
			return 'by clicking on a backdrop';
		} else {
			return `with: ${reason}`;
		}
	}
}
 
 
 