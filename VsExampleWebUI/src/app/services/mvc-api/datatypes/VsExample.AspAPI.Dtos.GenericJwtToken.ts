/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-04-14 12:52:15.996
 */
import { RoleEnum } from '../enums/VsExample.AspAPI.Dtos.RoleEnum';
export interface GenericJwtToken {
	Id?: string;
	Roles?: RoleEnum[];
	Name?: string;
	Token?: string;
	Valid?: boolean;
	ExpiringDate?: string;
}
