import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { LocalService } from '../../../core/services/local.service';
import { ApiService } from '../../../core/services/api.service';
import { UserResponse } from '../../../core/models/response/UserResponse';

import Swal from 'sweetalert2'


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  user: UserResponse

  constructor(private router: Router, private localService: LocalService, private apiService: ApiService){
    this.user = JSON.parse(this.localService.get('user'))
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
