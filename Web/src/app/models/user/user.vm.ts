import { Address } from './address.vm';
import { Lookup } from './../lookup.model'

export class User {
  firstNameEn: string
  lastNameEn: string
  emailAddress: string
  password: string
  isDefaultPassword: boolean
  confirmPassword: string
  iDN: string
  dateOfBirth?: Date;
  gender?: number;
  userRoles: any[] = []
  userApplications: any[] = []
  street: string
  city: string
  createdBy: string
  createdDate?: Date;
  modifiedBy: string
  modifiedDate?: Date;
  isDeleted: boolean
}



export class NoAuthUser {
  firstNameEn: string
  lastNameEn: string
  emailAddress: string
  password: string
  confirmPassword: string;
  dateOfBirth?: Date;
  gender: Lookup;
  street: string
  city: string
  country: Lookup
  userType:Lookup;
  id?: number;
}
