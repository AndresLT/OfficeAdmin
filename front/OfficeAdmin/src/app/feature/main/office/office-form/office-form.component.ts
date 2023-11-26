import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatSelectModule} from '@angular/material/select';
import { Currency } from '../../../../core/models/Currency';
import { ApiService } from '../../../../core/services/api.service';
import { MatButtonModule } from '@angular/material/button';
import { CreateOfficeRequest } from '../../../../core/models/request/CreateOfficeRequest';
import { LocalService } from '../../../../core/services/local.service';
import { UserResponse } from '../../../../core/models/response/UserResponse';
import { ModifyOfficeRequest } from '../../../../core/models/request/ModifyOfficeRequest';

@Component({
  selector: 'app-office-form',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule,MatCheckboxModule,MatSelectModule, MatButtonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './office-form.component.html',
  styleUrl: './office-form.component.scss'
})
export class OfficeFormComponent implements OnInit {

  createForm = new FormGroup({
    code: new FormControl('', Validators.required),
    identification: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    currency: new FormControl('', Validators.required),
  })

  updateForm = new FormGroup({
    code: new FormControl('', Validators.required),
    identification: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    currency: new FormControl('', Validators.required),
    active: new FormControl('', Validators.required)
  })

  currencies: Currency[] = []

  User: UserResponse

  constructor(public dialogRef: MatDialogRef<OfficeFormComponent>,@Inject(MAT_DIALOG_DATA) public data: any, private apiService: ApiService, private localService: LocalService) {
    this.User = JSON.parse(this.localService.get('user'))
  }
  ngOnInit(): void {
    console.log('data', this.data);
    this.getCurrencies(false)
    if(this.data.action == 'modify'){
      this.setUpdateFields()
    }

  }

  getCurrencies(all: boolean){
    this.apiService.get<Currency[]>('Currency/GetCurrencies/'+all).subscribe(res =>{
      this.currencies = res.result
      this.apiService.loading = false
    })
  }

  createOffice(){
    let newOffice : CreateOfficeRequest = {
      code: +this.createForm.controls['code'].value!,
      identification: this.createForm.controls['identification'].value!,
      description: this.createForm.controls['description'].value!,
      address: this.createForm.controls['address'].value!,
      currency: +this.createForm.controls['currency'].value!,
      username: this.User.username
    }

    this.apiService.post<string>('Office/CreateOffice', newOffice).subscribe(async res => {
      this.apiService.showMessages(res)
      await new Promise(f => setTimeout(f, 2000));
      if(res.status == "success"){
        this.dialogRef.close('success')
      }
    })
  }

  setUpdateFields(){
    this.updateForm.controls['code'].setValue(this.data.dataToUpdate.code)
    this.updateForm.controls['identification'].setValue(this.data.dataToUpdate.identification)
    this.updateForm.controls['description'].setValue(this.data.dataToUpdate.description)
    this.updateForm.controls['address'].setValue(this.data.dataToUpdate.address)
    this.updateForm.controls['currency'].setValue(this.data.dataToUpdate.currency)
    this.updateForm.controls['active'].setValue(this.data.dataToUpdate.active)
  }

  updateOffice(){
    let office : ModifyOfficeRequest = {
      code: +this.updateForm.controls['code'].value!,
      identification: this.updateForm.controls['identification'].value!,
      description: this.updateForm.controls['description'].value!,
      address: this.updateForm.controls['address'].value!,
      currency: +this.updateForm.controls['currency'].value!,
      username: this.User.username,
      active: JSON.parse(this.updateForm.controls['active'].value!),
      id: this.data.dataToUpdate.id
    }


    this.apiService.post<string>('Office/ModifyOffice', office).subscribe(async res => {
      this.apiService.showMessages(res)
      await new Promise(f => setTimeout(f, 2000));
      if(res.status == "success"){
        this.dialogRef.close('success')
      }
    })
  }

}
