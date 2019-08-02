import React, { useState } from 'react';
import {
    CreateButton,
    Dropdown,
    Loading,
    SearchInputField,
    SnackbarMessage,
    Title,
    ErrorCard,
    useSearch
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

function SerialNumberReissue({ errorMessage, fetchSerialNumbers, loading }) {
    const [searchTerm, setSearchTerm] = useState(null);

    useSearch(fetchSerialNumbers, searchTerm, null, 'sernosNumber');

    const handleSearchTermChange = (...args) => {
        setSearchTerm(args[1]);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                {errorMessage && <ErrorCard errorMessage={errorMessage} />}
                <Grid item xs={12}>
                    <Title text="Reissue Serial Numbers" />
                </Grid>
                <Grid item xs={4}>
                    <SearchInputField
                        label="Search by Serial Number"
                        fullWidth
                        placeholder="Serial Number"
                        onChange={handleSearchTermChange}
                        propertyName="searchTerm"
                        type="number"
                        value={searchTerm}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

export default SerialNumberReissue;
