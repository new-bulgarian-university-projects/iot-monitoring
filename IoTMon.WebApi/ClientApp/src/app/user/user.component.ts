import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { User } from '../auth/models/user.model';
import { UserService } from './user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.less']
})
export class UserComponent implements OnInit, OnDestroy {
  public userId: string;
  public user: User;
  private httpSub: Subscription = new Subscription();

  constructor(private router: ActivatedRoute,
    private userService: UserService) { }

  ngOnInit() {
    this.userId = this.router.snapshot.params['userId'];
    const sub = this.userService.getUser(this.userId)
      .subscribe((resp: User) => {
        this.user = resp;
      });
    this.httpSub.add(sub);
  }

  ngOnDestroy() {
    this.httpSub.unsubscribe();
  }

}
