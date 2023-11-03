export class UserVM{
    id: string = '';
    email: string = '';
    userName: string = '';
   fullName: string = '';
    phoneNumber: string = '';
    picture: string = '';
    creatorUserId : string = '';
    creationTime: Date;
    lastModifierUserId : number;
    lastModificationTime: Date;
    deleterUserId : string = '';
    deletionTime: Date;
    macAddress: string = '';
    fCMToken: string = '';
    password: string = '';
    twoFactorEnabled :boolean;
    status: number;
}