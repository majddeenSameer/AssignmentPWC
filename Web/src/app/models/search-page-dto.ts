import { environment } from '../../environments/environment';
import { OrderDirection } from './order-direction';

export class SearchPageDto<TDto> {
  constructor() {
    this.pageSize = environment.pageSize;
  };

  criteria: TDto = {} as TDto;
  orderDirection: OrderDirection;
  page: number;
  pageSize: number = environment.pageSize;
  sortBy?: string = environment.sortBy;
}
