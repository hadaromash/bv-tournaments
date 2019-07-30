import React, { useState } from "react";

const SelectionForm = ({ tournaments, categories, handleSubmit }) => {
  const [values, setValues] = useState({
    tournament: tournaments[0],
    category: categories[0]
  });

  const handleTournamentChange = event => {
    setValues({
      ...values,
      tournament: event.target.value
    });
  };

  const handleCategoryChange = event => {
    setValues({
      ...values,
      category: event.target.value
    });
  };

  const handleSubmitInternal = event => {
    handleSubmit(values);
    event.preventDefault();
  };

  const toursOptions = tournaments.map(tour => (
    <option value={tour}>{tour.name}</option>
  ));

  const catOptions = categories.map(cat => <option value={cat}>{cat}</option>);

  return (
    <form onSubmit={event => handleSubmitInternal(event)}>
      <label>
        Select tournament:
        <select value={values.tournament} onChange={handleTournamentChange}>
          {toursOptions}
        </select>
      </label>
      <label>
        Select category:
        <select value={values.category} onChange={handleCategoryChange}>
          {catOptions}
        </select>
      </label>
      <input type="submit" value="Submit" />
    </form>
  );
};

export default SelectionForm;
