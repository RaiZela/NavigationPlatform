import { FilterMatchMode } from "primereact/api";
import { Column } from "primereact/column";
import { DataTable, type DataTableFilterMeta } from "primereact/datatable";
import { useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { IconField } from "primereact/iconfield";
import { InputIcon } from "primereact/inputicon";

export default function JourneyList() {
  const [customers, setCustomers] = useState<any[]>([]);
  const [filters, setFilters] = useState<DataTableFilterMeta>({
    global: { value: null, matchMode: FilterMatchMode.CONTAINS },
    name: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
    "country.name": { value: null, matchMode: FilterMatchMode.STARTS_WITH },
    representative: { value: null, matchMode: FilterMatchMode.IN },
    status: { value: null, matchMode: FilterMatchMode.EQUALS },
    verified: { value: null, matchMode: FilterMatchMode.EQUALS },
  });
  const [loading, setLoading] = useState<boolean>(true);
  const [globalFilterValue, setGlobalFilterValue] = useState<string>("");
  const [representatives] = useState<any[]>([
    { StartLocation: "Amy Elsner", image: "amyelsner.png" },
    { StartLocation: "Anna Fali", image: "annafali.png" },
    { StartLocation: "Asiya Javayant", image: "asiyajavayant.png" },
    { StartLocation: "Bernardo Dominic", image: "bernardodominic.png" },
    { StartLocation: "Elwin Sharvill", image: "elwinsharvill.png" },
    { StartLocation: "Ioni Bowcher", image: "ionibowcher.png" },
    { StartLocation: "Ivan Magalhaes", image: "ivanmagalhaes.png" },
    { StartLocation: "Onyama Limba", image: "onyamalimba.png" },
    { StartLocation: "Stephen Shaw", image: "stephenshaw.png" },
    { StartLocation: "XuXue Feng", image: "xuxuefeng.png" },
    { StartLocation: "Amy Elsner", image: "amyelsner.png" },
    { StartLocation: "Anna Fali", image: "annafali.png" },
    { StartLocation: "Asiya Javayant", image: "asiyajavayant.png" },
    { StartLocation: "Bernardo Dominic", image: "bernardodominic.png" },
    { StartLocation: "Elwin Sharvill", image: "elwinsharvill.png" },
    { StartLocation: "Ioni Bowcher", image: "ionibowcher.png" },
    { StartLocation: "Ivan Magalhaes", image: "ivanmagalhaes.png" },
    { StartLocation: "Onyama Limba", image: "onyamalimba.png" },
    { StartLocation: "Stephen Shaw", image: "stephenshaw.png" },
    { StartLocation: "XuXue Feng", image: "xuxuefeng.png" },
    { StartLocation: "Amy Elsner", image: "amyelsner.png" },
    { StartLocation: "Anna Fali", image: "annafali.png" },
    { StartLocation: "Asiya Javayant", image: "asiyajavayant.png" },
    { StartLocation: "Bernardo Dominic", image: "bernardodominic.png" },
    { StartLocation: "Elwin Sharvill", image: "elwinsharvill.png" },
    { StartLocation: "Ioni Bowcher", image: "ionibowcher.png" },
    { StartLocation: "Ivan Magalhaes", image: "ivanmagalhaes.png" },
    { StartLocation: "Onyama Limba", image: "onyamalimba.png" },
    { StartLocation: "Stephen Shaw", image: "stephenshaw.png" },
    { StartLocation: "XuXue Feng", image: "xuxuefeng.png" },
    { StartLocation: "Amy Elsner", image: "amyelsner.png" },
    { StartLocation: "Anna Fali", image: "annafali.png" },
    { StartLocation: "Asiya Javayant", image: "asiyajavayant.png" },
    { StartLocation: "Bernardo Dominic", image: "bernardodominic.png" },
    { StartLocation: "Elwin Sharvill", image: "elwinsharvill.png" },
    { StartLocation: "Ioni Bowcher", image: "ionibowcher.png" },
    { StartLocation: "Ivan Magalhaes", image: "ivanmagalhaes.png" },
    { StartLocation: "Onyama Limba", image: "onyamalimba.png" },
    { StartLocation: "Stephen Shaw", image: "stephenshaw.png" },
    { StartLocation: "XuXue Feng", image: "xuxuefeng.png" },
    { StartLocation: "Amy Elsner", image: "amyelsner.png" },
    { StartLocation: "Anna Fali", image: "annafali.png" },
    { StartLocation: "Asiya Javayant", image: "asiyajavayant.png" },
    { StartLocation: "Bernardo Dominic", image: "bernardodominic.png" },
    { StartLocation: "Elwin Sharvill", image: "elwinsharvill.png" },
    { StartLocation: "Ioni Bowcher", image: "ionibowcher.png" },
    { StartLocation: "Ivan Magalhaes", image: "ivanmagalhaes.png" },
    { StartLocation: "Onyama Limba", image: "onyamalimba.png" },
    { StartLocation: "Stephen Shaw", image: "stephenshaw.png" },
    { StartLocation: "XuXue Feng", image: "xuxuefeng.png" },
  ]);
  const [statuses] = useState<string[]>([
    "unqualified",
    "qualified",
    "new",
    "negotiation",
    "renewal",
  ]);

  useEffect(() => {
    setCustomers(getCustomers(representatives));
    setLoading(false);
  }, []); // eslint-disable-line react-hooks/exhaustive-deps

  const getCustomers = (data: any[]) => {
    return [...(data || [])].map((d) => {
      // @ts-ignore
      d.date = new Date(d.date);

      return d;
    });
  };

  const onGlobalFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    let _filters = { ...filters };

    // @ts-ignore
    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };

  return (
    <div>
      <h1>Journey List</h1>
      <DataTable
        value={customers}
        paginator
        rows={10}
        dataKey="id"
        filters={filters}
        filterDisplay="row"
        loading={loading}
        globalFilterFields={[
          "StartLocation",
          "StartTime",
          "ArrivalLocation",
          "ArrivalTime",
          "TransportType",
          "DistanceKm",
        ]}
        emptyMessage="No customers found."
      >
        <Column
          field="StartLocation"
          header="StartLocation"
          filter
          filterPlaceholder="Search by StartLocation"
          style={{ minWidth: "12rem" }}
        />

        <Column
          field="StartTime"
          header="StartTime"
          filter
          filterPlaceholder="Search by StartTime"
          style={{ minWidth: "12rem" }}
        />

        <Column
          field="ArrivalLocation"
          header="ArrivalLocation"
          filter
          filterPlaceholder="Search by ArrivalLocation"
          style={{ minWidth: "12rem" }}
        />

        <Column
          field="ArrivalTime"
          header="ArrivalTime"
          filter
          filterPlaceholder="Search by ArrivalTime"
          style={{ minWidth: "12rem" }}
        />

        <Column
          field="TransportType"
          header="TransportType"
          filter
          filterPlaceholder="Search by TransportType"
          style={{ minWidth: "12rem" }}
        />

        <Column
          field="DistanceKm"
          header="DistanceKm"
          filter
          filterPlaceholder="Search by DistanceKm"
          style={{ minWidth: "12rem" }}
        />
      </DataTable>
    </div>
  );
}
