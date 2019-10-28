import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, CreateButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFails({ items, fetchItems, loading, clearSearch, history }) {
    const forecastItems = items.map(item => ({
        ...item,
        name: item.id,
        description: item.reportedFault
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Fragment>
                        <CreateButton createUrl="/production/quality/assembly-fails/create" />
                    </Fragment>
                    <Typeahead
                        items={forecastItems}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Search For Assembly Fail"
                        history={history}
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
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

AssemblyFails.defaultProps = {
    loading: false
};

export default AssemblyFails;
