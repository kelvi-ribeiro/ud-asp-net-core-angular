import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  baseURL = 'http://localhost:5000/api/user/'
  jwtHelper = new JwtHelperService

  constructor(private http: HttpClient) { }

  login(){

  }

  register(){

  }

  loggedIn(){
    return false
  }


}
