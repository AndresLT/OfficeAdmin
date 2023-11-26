import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-office-form',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './office-form.component.html',
  styleUrl: './office-form.component.scss'
})
export class OfficeFormComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
  ngOnInit(): void {
    console.log('data', this.data);

  }


}
