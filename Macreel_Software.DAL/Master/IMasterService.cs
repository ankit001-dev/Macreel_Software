using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macreel_Software.Models.Master;

namespace Macreel_Software.DAL.Master
{
    public interface IMasterService
    {
        Task<int> InsertRole(role data);
        Task<(List<role> Data, int TotalRecords)> getAllRole( string? searchTerm,int? pageNumber,int? pageSize);

        Task<List<role>> getAllRoleById(int id);

        Task<bool> deleteRoleById(int id);
        Task<int> insertDepartment(department data);
        Task<(List<department> Data, int TotalRecords)> getAllDepartment(string? searchTerm,int? pageNumber,
    int? pageSize);

        Task<List<department>> getAllDepartmentById(int id);
        Task<bool> deleteDepartmentById(int id);

        Task<int> InsertDesignation(designation data);

        Task<(List<designation> Data, int TotalRecords)> getAllDesignation(string? searchTerm,int? pageNumber,
     int? pageSize);


        Task<List<designation>> getAllDesignationById(int id);

        Task<bool> deleteDesignationById(int id);


    }
}
