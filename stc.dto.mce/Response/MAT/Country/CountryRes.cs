using System;

namespace stc.dto.mce.Response
{
    public class CountryRes
    {
        public int country_id {  get; set; }
        public string country_name {  get; set; }
        public string country_code {  get; set; }
        public bool is_default {  get; set; }
        public int created_user {  get; set; }
        public DateTime created_date {  get; set; }
        public int updated_user {  get; set; }
        public DateTime updated_date {  get; set; }
    }
}
