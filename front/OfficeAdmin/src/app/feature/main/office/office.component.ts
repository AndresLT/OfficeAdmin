import { MatIconModule } from '@angular/material/icon';
import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Office } from '../../../core/models/Office';
import { ApiService } from '../../../core/services/api.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { StatusPipe } from '../../../shared/pipes/status.pipe';
import { CurrencyCodePipe } from '../../../shared/pipes/currency-code.pipe';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { OfficeFormComponent } from './office-form/office-form.component';

@Component({
  selector: 'app-office',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatFormFieldModule, MatPaginatorModule, MatInputModule,MatButtonModule,MatIconModule, MatSortModule, StatusPipe, CurrencyCodePipe, MatDialogModule],
  templateUrl: './office.component.html',
  styleUrl: './office.component.scss'
})
export class OfficeComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['Code', 'Identification', 'Description', 'Address','Currency','Active','Modify'];
  dataSource: MatTableDataSource<Office> =new MatTableDataSource<Office>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private apiService: ApiService, public dialog: MatDialog){
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
    this.getOffices(true)
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog(action: string, dataToUpdate?: any) {
    this.dialog.open(OfficeFormComponent, {
      data: {
        action: action,
        dataToUpdate: dataToUpdate
      },
    }).afterClosed().subscribe(res => {
      if(res == "success"){
        this.getOffices(true)
      }
    });
  }

  getOffices(all: boolean){
    this.apiService.get<Office[]>('Office/GetOffices/'+all).subscribe(res =>{
      console.log('Sucursales', res.result);
      this.dataSource = new MatTableDataSource<Office>(res.result)
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.apiService.showMessages(res)
    })
  }

}
