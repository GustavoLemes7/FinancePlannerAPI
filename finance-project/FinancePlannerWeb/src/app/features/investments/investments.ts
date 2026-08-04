import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { CreateAccount } from '../../core/interfaces/account/create-account';
import { Modal } from '../../shared/components/modal/modal';
import { UpdateAccount } from '../../core/interfaces/account/update-account';
import { switchMap } from 'rxjs';
import { Investment } from '../../core/interfaces/investment/investment';
import { InvestmentService } from '../../core/services/investment/investment.service';
import { CreateInvestment } from '../../core/interfaces/investment/create-investment';
import { UpdateInvestment } from '../../core/interfaces/investment/update-investment';

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [
    CurrencyPipe,
    ReactiveFormsModule,
    Modal
  ],
  templateUrl: './investments.html',
  styleUrl: './investments.css'
})
export class Investments implements OnInit {

  private investmentService = inject(InvestmentService);

  private fb = inject(FormBuilder);
  private cdr = inject(ChangeDetectorRef);

  investments: Investment[] = [];

  showModal = false;

  loading = false;

  errorMessage = '';

  isEditMode = false;

  editingInvestment: Investment | null = null;

 investmentForm = this.fb.nonNullable.group({

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

  amount: [
    0,
    [
      Validators.required,
      Validators.min(0.01)
    ]
  ],

  interestRate: [
    0,
    [
      Validators.required,
      Validators.min(0)
    ]
  ],

  investmentDate: [
    '',
    [
      Validators.required
    ]
  ]

});

  ngOnInit(): void {

    this.loadInvestments();

  }

  loadInvestments(): void {

    this.investmentService.getAll().subscribe({

      next: (data) => {

        this.investments = data;
        this.cdr.detectChanges();

      },

      error: (err) => {

        console.error(err);

      }

    });

  }

  openModal(): void {

    this.isEditMode = false;
    this.editingInvestment = null;

    this.investmentForm.reset();

    this.showModal = true;
  }

  openEditModal(investment: Investment): void {

    this.isEditMode = true;
    this.editingInvestment = investment;

    this.investmentForm.patchValue({
        name: investment.name,
        type: investment.type,
        amount: investment.amount,
        interestRate: investment.interestRate,
        investmentDate: investment.investmentDate
    });

    this.showModal = true;
  }

  closeModal(): void {

    if (this.loading) {
      return;
    }

    this.showModal = false;

  }

  createInvestment(): void {

    if (this.investmentForm.invalid) {

      this.investmentForm.markAllAsTouched();

      return;

    }

    this.loading = true;

    this.errorMessage = '';

    const investment: CreateInvestment = this.investmentForm.getRawValue();

    this.investmentService.create(investment).subscribe({

      next: () => {

        this.investmentService.getAll().subscribe({

                next: (investments) => {

                    this.investments = investments;

                    this.loading = false;
                    this.showModal = false;

                    this.investmentForm.reset();

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

  updateInvestment(): void {

    if (!this.editingInvestment) {
        return;
    }

    this.loading = true;
    this.errorMessage = '';

    const data: UpdateInvestment = this.investmentForm.getRawValue();

    this.investmentService
        .update(this.editingInvestment.publicId, data)
        .pipe(
            switchMap(() => this.investmentService.getAll())
        )
        .subscribe({

            next: (investments) => {

                this.investments = investments ;

                this.loading = false;
                this.showModal = false;

                this.investmentForm.reset();

                this.editingInvestment = null;

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

  deleteInvestment(publicId: string): void {

    this.investmentService.delete(publicId).subscribe({

        next: () => {

            this.investments = this.investments.filter(
                investment => investment.publicId !== publicId
            );

            this.loadInvestments();

        },

        error: (err) => {

            console.error(err);

        }

    });

  }

  submitInvestment(): void {

    if (this.investmentForm.invalid) {

        this.investmentForm.markAllAsTouched();

        return;
    }

    if (this.isEditMode) {

        this.updateInvestment();

    } else {

        this.createInvestment();

    }

}

}