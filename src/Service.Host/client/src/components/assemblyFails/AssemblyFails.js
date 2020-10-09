import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Typeahead,
    CreateButton,
    InputField,
    DatePicker
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Page from '../../containers/Page';

function AssemblyFails({ items, fetchItems, searchWithOptions, loading, clearSearch, history }) {
    const [searchParameters, setSearchParameters] = useState({
        partNumber: '',
        productId: '',
        circuitPart: '',
        boardPart: '',
        date: null
    });
    const handleFieldChange = (propertyName, newValue) => {
        setSearchParameters({ ...searchParameters, [propertyName]: newValue });
    };

    const forecastItems = items.map(item => ({
        ...item,
        name: `${item.id} - ${item.partNumber}`,
        description: item.reportedFault
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={9} />
                <Grid item xs={3}>
                    <CreateButton createUrl="/production/quality/assembly-fails/create" />
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="h2"> Search Assembly Fails</Typography>{' '}
                </Grid>
                <Grid item xs={12}>
                    <ExpansionPanel>
                        <ExpansionPanelSummary
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>Refined Search</Typography>
                        </ExpansionPanelSummary>
                        <ExpansionPanelDetails>
                            <Grid container spacing={3}>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Part Number"
                                        value={searchParameters.partNumber}
                                        propertyName="partNumber"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Circuit Part"
                                        value={searchParameters.circuitPart}
                                        propertyName="circuitPart"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        label="Board Part"
                                        value={searchParameters.boardPart}
                                        propertyName="boardPart"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        label="Product ID"
                                        value={searchParameters.productId}
                                        propertyName="productId"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={4}>
                                    <DatePicker
                                        value={searchParameters.date}
                                        label="Date"
                                        onChange={value => {
                                            setSearchParameters(a => ({
                                                ...a,
                                                date: value
                                            }));
                                        }}
                                    />
                                </Grid>
                                <Grid item xs={8} />

                                <Grid item xs={8} />
                                <Grid item xs={2}>
                                    <Button
                                        variant="outlined"
                                        onClick={() =>
                                            setSearchParameters({
                                                partNumber: '',
                                                productId: '',
                                                circuitPart: '',
                                                boardPart: '',
                                                date: null
                                            })
                                        }
                                    >
                                        Clear
                                    </Button>
                                </Grid>
                                <Grid item xs={2}>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        disabled={Object.values(searchParameters).every(v => !v)}
                                        onClick={() =>
                                            searchWithOptions(
                                                '',
                                                `&partNumber=${
                                                    searchParameters.partNumber
                                                }&productId=${
                                                    searchParameters.productId
                                                }&circuitPart=${
                                                    searchParameters.circuitPart
                                                }&boardPart=${
                                                    searchParameters.boardPart
                                                }&date=${searchParameters.date?.toISOString()}`
                                            )
                                        }
                                    >
                                        Search
                                    </Button>
                                </Grid>
                            </Grid>
                        </ExpansionPanelDetails>
                    </ExpansionPanel>
                </Grid>

                <Grid item xs={12}>
                    <Typeahead
                        items={forecastItems
                            .map(i => ({ ...i, name: i.name.toString() }))
                            .sort((a, b) => (a.id > b.id ? 1 : -1))}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title=""
                        history={history}
                        placeholder="search for a part or fault keyword"
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFails.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    searchWithOptions: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

AssemblyFails.defaultProps = {
    loading: false
};

export default AssemblyFails;
