import React, { useState } from "react";

const SelectionForm = ({ tournaments, categories, handleSubmit }) => {
  const [tournamentId, setTournamentId] = useState(tournaments[0].tournamentId);
  const [categoryId, setCategoryId] = useState(categories[0].id);

  const findTournament = tour => {
    return tour.tournamentId == tournamentId;
  }

  const findCategory = cat => {
    return cat.id == categoryId;
  }

  const handleSubmitInternal = event => {
    let tournament = tournaments.find(findTournament);
    let category = categories.find(findCategory);
    let values = {
      tournament: tournament,
      category: category
    };

    handleSubmit(values);
    event.preventDefault();
  };

  const toursOptions = tournaments.map(tour => (
    <option key={tour.tournamentId} value={tour.tournamentId}>{tour.name}</option>
  ));

  const catOptions = categories.map(cat => <option key={cat.id} value={cat.id}>{cat.displayName}</option>);

  return (
    <form onSubmit={event => handleSubmitInternal(event)}>
      <label>
        בחר טורניר:
        <select value={tournamentId} onChange={event => setTournamentId(event.target.value)}>
          {toursOptions}
        </select>
      </label>
      <label>
        בחר קטגוריה:
        <select value={categoryId} onChange={event => setCategoryId(event.target.value)}>
          {catOptions}
        </select>
      </label>
      <input type="submit" value="פנק אותו!" />
    </form>
  );
};

export default SelectionForm;
