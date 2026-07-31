import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { Transaction } from '../../core/interfaces/transaction/transaction';
import { Account } from '../../core/interfaces/account/account';

import { TransactionService } from '../../core/services/transaction/transaction.service';
import { AccountService } from '../../core/services/account/account.service';

import { Modal } from '../../shared/components/modal/modal';
import { UpdateTransaction } from '../../core/interfaces/transaction/update-transaction';
import { CreateTransaction } from '../../core/interfaces/transaction/create-transaction';

@Component({
  selector: 'app-transactions',
  standalone: true,

  imports: [
    ReactiveFormsModule,
    DecimalPipe,
    DatePipe,
    Modal
  ],

  templateUrl: './transactions.html',
  styleUrl: './transactions.css'
})
export class Transactions implements OnInit {

    private transactionService = inject(TransactionService);
    private accountService = inject(AccountService);
    private fb = inject(FormBuilder);
    private cdr = inject(ChangeDetectorRef);


    transactions: Transaction[] = [];

    accounts: Account[] = [];


    showCreateModal = false;

    isEditMode = false;

    isSaving = false;

    formError = '';

    selectedPublicId: string | null = null;


    transactionForm = this.fb.group({

        accountPublicId: ['', Validators.required],

        amount: this.fb.control<number | null>(null, {
        validators: [
            Validators.required,
            Validators.min(0.01)
        ]
        }),

        type: ['', Validators.required],

        category: ['', Validators.required],

        description: [
            '',
            Validators.required
        ],

        transactionDate: [
            '',
            Validators.required
        ]

    });


    ngOnInit(): void {

        this.loadTransactions();

        this.loadAccounts();

    }


    loadTransactions(): void {

        this.transactionService.getAll().subscribe({

            next: (data) => {

                this.transactions = data;
                this.cdr.detectChanges();

            },

            error: (err) => {

                console.error(err);

            }

        });

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

        this.selectedPublicId = null;

        this.formError = '';

        this.transactionForm.reset();

        this.showCreateModal = true;

    }


    openEditModal(transaction: Transaction): void {

        this.isEditMode = true;

        this.selectedPublicId = transaction.publicId;

        this.formError = '';

        this.transactionForm.patchValue({

            accountPublicId: transaction.accountPublicId,

            amount: transaction.amount,

            type: transaction.type,

            category: transaction.category,

            description: transaction.description,

            transactionDate: transaction.transactionDate
            

        });

        this.showCreateModal = true;

    }


    closeModal(): void {

        if (this.isSaving) {
            return;
        }

        this.showCreateModal = false;

        this.formError = '';

    }


    saveTransaction(): void {

        if (this.transactionForm.invalid) {

            this.transactionForm.markAllAsTouched();

            return;

        }


        this.isSaving = true;

        this.formError = '';


        const form = this.transactionForm.getRawValue();


        let request: CreateTransaction | UpdateTransaction;

        if (this.isEditMode) {
            request = {
                accountPublicId: form.accountPublicId!,
                amount: form.amount!,
                type: form.type!,
                category: form.category!,
                description: form.description!,
                transactionDate: form.transactionDate!
            };
        } else {
            request = {
                accountPublicId: form.accountPublicId!,
                amount: form.amount!,
                type: form.type!,
                category: form.category!,
                description: form.description!,
                transactionDate: form.transactionDate!
            };
        }


        if (this.isEditMode && this.selectedPublicId) {

            this.transactionService
                .update(this.selectedPublicId, request)
                .subscribe({

                    next: () => {

                        this.loadTransactions();

                        this.closeAfterSave();

                    },

                    error: (err) => {

                        console.error(err);

                        this.formError =
                            'Não foi possível atualizar a transação.';

                        this.isSaving = false;

                    }

                });

            return;

        }


        this.transactionService
            .create(request)
            .subscribe({

                next: () => {

                    this.loadTransactions();

                    this.closeAfterSave();

                },

                error: (err) => {

                    console.error(err);

                    this.formError =
                        'Não foi possível criar a transação.';

                    this.isSaving = false;

                }

            });

    }


    private closeAfterSave(): void {

        this.isSaving = false;

        this.showCreateModal = false;

        this.transactionForm.reset();

        this.selectedPublicId = null;

    }

    deleteTransaction(publicId: string): void {

    this.transactionService.delete(publicId).subscribe({

        next: () => {

            this.transactions = this.transactions.filter(
                transaction => transaction.publicId !== publicId
            );

            this.loadTransactions();

        },

        error: (err) => {

            console.error(err);

        }

    });

  }


}