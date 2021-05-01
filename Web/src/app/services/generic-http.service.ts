import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PagedDataDto } from '../models/paged-data-dto';
import { SearchPageDto } from '../models/search-page-dto';

export class GenericHttpService<TDto, TFilter>  {
  constructor(
    protected endpoint: string,
    protected http: HttpClient,
  ) { }

  public create(item: TDto): Observable<TDto> {
    return this.http.post<TDto>(`${environment.serverUrl}${this.endpoint}`, item);
  }

  public update(item: TDto): Observable<TDto> {
    return this.http.put<TDto>(`${environment.serverUrl}${this.endpoint}`, item);
  }

  get(id: number): Observable<TDto> {
    return this.http.get<TDto>(`${environment.serverUrl}${this.endpoint}/${id}`);
  }

  search(search: SearchPageDto<TFilter>): Observable<PagedDataDto<TDto>> {
    return this.http.post<PagedDataDto<TDto>>(`${environment.serverUrl}${this.endpoint}/search`, search);
  }

  delete(id: number) {
    return this.http.delete(`${environment.serverUrl}${this.endpoint}/${id}`);
  }

  public updateRange(item: TDto[]): Observable<TDto[]> {
    return this.http.post<TDto[]>(`${environment.serverUrl}${this.endpoint}/UpdateRange`, item);
  }
}
