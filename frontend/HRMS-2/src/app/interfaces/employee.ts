export interface Employees {
  id?: number;
  name?: string;
  positionId?: number;
  positionName?: string;
  status?: boolean;
  birthDate?: Date;
  email?: string;
  salary?: number;
  departmentId?: number;
  departmentName?: string;
  managerId?: number|null;
  managerName?: string|null;
  userId?: number;

}