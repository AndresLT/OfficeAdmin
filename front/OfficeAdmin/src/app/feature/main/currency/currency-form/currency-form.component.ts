import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-currency-form',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './currency-form.component.html',
  styleUrl: './currency-form.component.scss'
})
export class CurrencyFormComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
  ngOnInit(): void {
    console.log('data', this.data);

  }
}
