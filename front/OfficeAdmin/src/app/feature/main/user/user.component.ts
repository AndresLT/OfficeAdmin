import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { UserAdminResponse } from '../../../core/models/response/UserAdminResponse';
import { ApiService } from '../../../core/services/api.service';
import { StatusPipe } from '../../../shared/pipes/status.pipe';
import { MatDialog } from '@angular/material/dialog';
import { UserFormComponent } from './user-form/user-form.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule,MatTableModule, MatFormFieldModule, MatPaginatorModule, MatInputModule,MatButtonModule,MatIconModule, MatSortModule, StatusPipe],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent {
  displayedColumns: string[] = ['Username', 'Name', 'Lastname','Active','Modify'];
  dataSource: MatTableDataSource<UserAdminResponse> =new MatTableDataSource<UserAdminResponse>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private apiServive: ApiService, public dialog: MatDialog){
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
    this.getUsers()
  }

  openDialog(action: string, dataToUpdate? : any) {
    this.dialog.open(UserFormComponent, {
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

  getUsers(){
    this.apiServive.get<UserAdminResponse[]>('Admin/GetUsers').subscribe(res =>{
      console.log('Usuarios', res.result);
      this.dataSource = new MatTableDataSource<UserAdminResponse>(res.result)
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.apiServive.loading = false
    })
  }
}
