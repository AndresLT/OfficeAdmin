import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LocalService } from '../../../core/services/local.service';
import { ApiService } from '../../../core/services/api.service';
import { UserResponse } from '../../../core/models/response/UserResponse';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatProgressBarModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  user: UserResponse

  constructor(private router: Router, private localService: LocalService, private apiService: ApiService){
    this.user = JSON.parse(this.localService.get('user'))
  }
  ngOnInit(): void {

  }

  logout(){
    this.apiService.get('Admin/Logout/'+this.user.username).subscribe(res => {
      Swal.fire({
        icon:'success',
        text: res.message
      })
      this.localService.remove('user')
      this.router.navigate([''])
    this.apiService.loading = false
    })
  }
}
