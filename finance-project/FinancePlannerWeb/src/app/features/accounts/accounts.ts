import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CurrencyPipe, NgFor } from '@angular/common';
import { Account } from '../../core/interfaces/account/account';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [
    CurrencyPipe,
  ],
  templateUrl: './accounts.html',
  styleUrl: './accounts.css'
})
export class Accounts implements OnInit {

  private accountService = inject(AccountService);
  private cdr = inject(ChangeDetectorRef);

  accounts: Account[] = [];

  ngOnInit(): void {

    console.log('Accounts carregou');

    this.accountService.getAll().subscribe({

      next: (data) => {

        console.log('Recebi:', data);

        this.accounts = data;

        this.cdr.detectChanges();

      }

    });

  }

}