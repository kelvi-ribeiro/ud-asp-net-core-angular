import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(
    private router: Router,
    public authService: AuthService

  ) { }

  ngOnInit() {
  }

  entrar() {
    this.router.navigate(['/user/login'])
  }

  loggedIn() {
    return this.authService.loggedIn()
  }
  logout() {
    localStorage.removeItem('token')
    this.router.navigate(['/user/login'])

  }

}
