import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Account } from '../../core/interfaces/account/account';
import { CreateAccount } from '../../core/interfaces/account/create-account';
import { AccountService } from '../../core/services/account.service';
import { Modal } from '../../shared/components/modal/modal';
import { UpdateAccount } from '../../core/interfaces/account/update-account';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [
    CurrencyPipe,
    ReactiveFormsModule,
    Modal
  ],
  templateUrl: './accounts.html',
  styleUrl: './accounts.css'
})
export class Accounts implements OnInit {

  private accountService = inject(AccountService);

  private fb = inject(FormBuilder);
  private cdr = inject(ChangeDetectorRef);

  accounts: Account[] = [];

  showCreateModal = false;

  loading = false;

  errorMessage = '';

  isEditMode = false;

  editingAccount: Account | null = null;

  accountForm = this.fb.nonNullable.group({

    name: [
      '',
      [
        Validators.required,
        Validators.maxLength(100)
      ]
    ],

    type: [
      '',
      [
        Validators.required
      ]
    ],

    initialBalance: [
      0,
      [
        Validators.required
      ]
    ]

  });

  ngOnInit(): void {

    this.loadAccounts();

  }

  loadAccounts(): void {

    this.accountService.getAll().subscribe({

      next: (data) => {

        this.accounts = data;
        this.cdr.detectChanges();

      },

      error: (err) => {

        console.error(err);

      }

    });

  }

  openCreateModal(): void {

    this.isEditMode = false;
    this.editingAccount = null;

    this.accountForm.reset();

    this.showCreateModal = true;
  }

  openEditModal(account: Account): void {

    this.isEditMode = true;
    this.editingAccount = account;

    this.accountForm.patchValue({
        name: account.name,
        type: account.type,
        initialBalance: account.initialBalance
    });

    this.showCreateModal = true;
  }

  closeCreateModal(): void {

    if (this.loading) {
      return;
    }

    this.showCreateModal = false;

  }

  createAccount(): void {

    if (this.accountForm.invalid) {

      this.accountForm.markAllAsTouched();

      return;

    }

    this.loading = true;

    this.errorMessage = '';

    const account: CreateAccount = this.accountForm.getRawValue();

    this.accountService.create(account).subscribe({

      next: () => {

        this.accountService.getAll().subscribe({

                next: (accounts) => {

                    this.accounts = accounts;

                    this.loading = false;
                    this.showCreateModal = false;

                    this.accountForm.reset();

                },

                error: (err) => {

                    console.error('Erro ao carregar contas:', err);

                    this.loading = false;
                    this.errorMessage =
                        'A conta foi criada, mas não foi possível atualizar a lista.';

                }
          })
      },

    });

  }

  updateAccount(): void {

    if (!this.editingAccount) {
        return;
    }

    this.loading = true;
    this.errorMessage = '';

    const data: UpdateAccount = this.accountForm.getRawValue();

    this.accountService
        .update(this.editingAccount.publicId, data)
        .pipe(
            switchMap(() => this.accountService.getAll())
        )
        .subscribe({

            next: (accounts) => {

                this.accounts = accounts;

                this.loading = false;
                this.showCreateModal = false;

                this.accountForm.reset();

                this.editingAccount = null;

            },

            error: (err) => {

                console.error('Erro ao atualizar conta:', err);

                this.loading = false;

                this.errorMessage =
                    err?.error?.message ??
                    'Não foi possível atualizar a conta.';

            }

        });

  }

  deleteAccount(publicId: string): void {

    this.accountService.delete(publicId).subscribe({

        next: () => {

            this.accounts = this.accounts.filter(
                account => account.publicId !== publicId
            );

            this.loadAccounts();

        },

        error: (err) => {

            console.error(err);

        }

    });

  }

  submitAccount(): void {

    if (this.accountForm.invalid) {

        this.accountForm.markAllAsTouched();

        return;
    }

    if (this.isEditMode) {

        this.updateAccount();

    } else {

        this.createAccount();

    }

}

}