import { TestBed } from '@angular/core/testing';

import { InvestmentService } from './investment.service';

describe('Investment', () => {
  let service: InvestmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvestmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
