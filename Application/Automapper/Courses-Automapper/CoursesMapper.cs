using Application.Automapper.Courses.Query;
using AutoMapper;
using Domain.Entities;

namespace Application.Automapper.Courses_Automapper
{
    public class CoursesMapper:Profile
    {
        
        public CoursesMapper()
        {
            CreateMap<Course, CourseInformationDto>()
                .ForMember(property => property.Professors,
                    mapping => mapping.MapFrom(finallyProperty =>
                    finallyProperty.ProfessorLink.Select(getInformation => 
                    getInformation.Professor).ToList()))
                
                .ForMember(property => property.Comments, mapping => 
                mapping.MapFrom(finallyProperty => finallyProperty.AsignedComments))
                .ForMember(property => property.Price, mapping =>
                mapping.MapFrom(finallyProperty => finallyProperty.PriceAsigned));


            CreateMap<Course_Professor, CourseProfessorRelationshipDto>()
                .ForMember(property => property.IdProfessor,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdProfessor))
                 .ForMember(property => property.IdCourse,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdCourse));


            CreateMap<Professor, ProfessorInformationDto>()
                .ForMember(property => property.IdProfessor,
                mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdProfessor))
                 .ForMember(property => property.Name,
                mapping => mapping.MapFrom(finallyProperty => finallyProperty.Name))
                  .ForMember(property => property.LastName,
                mapping => mapping.MapFrom(finallyProperty => finallyProperty.LastName))
                   .ForMember(property => property.Grade,
                mapping => mapping.MapFrom(finallyProperty => finallyProperty.Grade));


            CreateMap<Comments, CommentsDto>()
                .ForMember(property => property.IdCourse,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdCourse))
               .ForMember(property => property.IdComment,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdComment))
               .ForMember(property => property.Student,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.Student))
               .ForMember(property => property.Score,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.Score))
               .ForMember(property => property.CommentText,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.CommentText));

            CreateMap<Price, PriceDto>()
                .ForMember(property => property.IdCourse,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdCourse))
                 .ForMember(property => property.IdPrice,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.IdPrice))
                .ForMember(property => property.CurrentPrice,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.CurrentPrice))
                .ForMember(property => property.PromotionalPrice,
                  mapping => mapping.MapFrom(finallyProperty => finallyProperty.PromotionalPrice));
               

        }
    }
}
