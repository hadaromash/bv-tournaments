import React, { useState } from "react";
import styled from "styled-components";

const SelectionForm = ({ tournaments, categories, handleSubmit }) => {
  const [tournamentId, setTournamentId] = useState(tournaments[0].tournamentId);
  const [categoryId, setCategoryId] = useState(categories[0].id);

  const findTournament = tour => {
    return tour.tournamentId == tournamentId;
  };

  const findCategory = cat => {
    return cat.id == categoryId;
  };

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
    <option key={tour.tournamentId} value={tour.tournamentId}>
      {tour.name}
    </option>
  ));

  const catOptions = categories.map(cat => (
    <option key={cat.id} value={cat.id}>
      {cat.displayName}
    </option>
  ));

  return (
    <form onSubmit={event => handleSubmitInternal(event)}>
      <FormContainer>
        <label>בחר\י טורניר:</label>
        <Select
          value={tournamentId}
          onChange={event => setTournamentId(event.target.value)}
        >
          {toursOptions}
        </Select>
        <label>בחר\י קטגוריה:</label>
        <Select
          value={categoryId}
          onChange={event => setCategoryId(event.target.value)}
        >
          {catOptions}
        </Select>
        <input type="submit" value="פנק אותו!" />
      </FormContainer>
    </form>
  );
};

export default SelectionForm;

const FormContainer = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;

  & > * {
    margin-left: 2rem;
    margin-bottom: 1rem;
    margin-top: 1rem;
  }
`;

const Select = styled.select`
`;
