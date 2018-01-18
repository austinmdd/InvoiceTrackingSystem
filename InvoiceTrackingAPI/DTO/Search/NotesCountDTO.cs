using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class NotesCountDTO
    {
        public int Count { get; set; }
        public List<NotesDTO> Notes = new List<NotesDTO>();
        public NotesCountDTO(List<ITS_Notes> notes, int records = 0)
        {
            Count = records;
            foreach (ITS_Notes note in notes)
            {
                Notes.Add(new NotesDTO(note));
            }
        }
    }
}