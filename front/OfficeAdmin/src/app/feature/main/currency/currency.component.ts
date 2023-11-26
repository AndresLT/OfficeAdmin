import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Currency } from '../../../core/models/Currency';
import { ApiService } from '../../../core/services/api.service';
import { StatusPipe } from '../../../shared/pipes/status.pipe';
import { MatDialog } from '@angular/material/dialog';
import { CurrencyFormComponent } from './currency-form/currency-form.component';

@Component({
  selector: 'app-currency',
  standalone: true,
  imports: [CommonModule,MatTableModule, MatFormFieldModule, MatPaginatorModule, MatInputModule,MatButtonModule,MatIconModule, MatSortModule, StatusPipe],
  templateUrl: './currency.component.html',
  styleUrl: './currency.component.scss'
})
export class CurrencyComponent {
  displayedColumns: string[] = ['Code', 'Description', 'Active','Modify'];
  dataSource: MatTableDataSource<Currency> =new MatTableDataSource<Currency>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private apiServive: ApiService, public dialog: MatDialog){
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
    this.getCurrencies(true)
  }

  openDialog(action: string, dataToUpdate?: any) {
    this.dialog.open(CurrencyFormComponent, {
      data: {
        action: action,
        dataToUpdate: dataToUpdate
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getCurrencies(all: boolean){
    this.apiServive.get<Currency[]>('Currency/GetCurrencies/'+all).subscribe(res =>{
      console.log('Monedas', res.result);
      this.dataSource = new MatTableDataSource<Currency>(res.result)
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.apiServive.loading = false
    })
  }
}
