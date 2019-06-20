import { Component, OnInit, OnDestroy } from '@angular/core';
import { RegisterUser } from '../models/registerUser.model';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { SignupUser } from '../models/signupUser.model';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.less']
})
export class SignupComponent implements OnInit, OnDestroy {
  user: RegisterUser = new RegisterUser();
  subs: Subscription = new Subscription();
  
  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
  }

  onSubmit() {
    console.log(this.user);
    try {
      const httpSub = this.authService.signupUser(this.user)
        .subscribe((user: SignupUser) => {
          console.log(`${user.email} created successfully `);
          this.router.navigate(['signin']);
        }, err => console.log(err));

      this.subs.add(httpSub);
    } catch (error) {
      console.log(error);
    }
  }

  ngOnDestroy() {
    if (this.subs){
      this.subs.unsubscribe();
    }
  }
}
