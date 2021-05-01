import { NoAuthUser } from '../user/user.vm';
import { Lookup } from '../lookup.model'

export class Complaint {
  noAuthUser: NoAuthUser
  title: string
  details: string
  requestStatus: Lookup;
}
