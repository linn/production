import React, { useState, useCallback, useEffect, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    InputField,
    Loading,
    SearchInputField,
    Title,
    ErrorCard,
    useSearch,
    utilities
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

function LabelReprint({ itemErrors, fetchSerialNumbers, serialNumbers, serialNumbersLoading }) {
    const [searchTerm, setSearchTerm] = useState(null);
    const [sernosGroups, setSernosGroups] = useState([]);
    const [selectedSernosGroup, setSelectedSernosGroup] = useState('');
    const [articleNumber, setArticleNumber] = useState(null);

    const classes = useStyles();

    useSearch(fetchSerialNumbers, searchTerm, null, 'sernosNumber');

    const selectSerialNumber = useCallback(
        sernosGroup => {
            const sernos = serialNumbers.find(s => s.sernosGroup === sernosGroup);
            if (sernos) {
                setArticleNumber(sernos.articleNumber);
            }
        },
        [serialNumbers]
    );

    useEffect(() => {
        if (serialNumbers && serialNumbers.length) {
            const sortedGroups = serialNumbers.reduce((groups, serialNumber) => {
                if (!groups.includes(serialNumber.sernosGroup)) {
                    groups.push(serialNumber.sernosGroup);
                }
                return utilities.sortList(groups);
            }, []);

            setSernosGroups(sortedGroups);
            setSelectedSernosGroup(sortedGroups[0] || '');
            selectSerialNumber(sortedGroups[0]);
        }
    }, [serialNumbers, selectSerialNumber]);

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'searchTerm') {
            setSearchTerm(newValue);
            return;
        }

        if (propertyName === 'selectedSernosGroup') {
            setSelectedSernosGroup(newValue);
            selectSerialNumber(newValue);
            return;
        }

        if (propertyName === 'articleNumber') {
            setArticleNumber(newValue);
        }
    };

    const handlePrintMacAddressButtonClick = () => {};

    const handlePrintAllButtonClick = () => {};

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Reprint Product Labels" />
                </Grid>
                {itemErrors &&
                    itemErrors.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={`${itemError.item} ${itemError.statusText}`} />
                        </Grid>
                    ))}
                <Grid item xs={3}>
                    <SearchInputField
                        label="Search by Serial Number"
                        fullWidth
                        placeholder="Serial Number"
                        onChange={handleFieldChange}
                        propertyName="searchTerm"
                        type="number"
                        value={searchTerm}
                    />
                </Grid>
                <Grid item xs={9} />
                {serialNumbersLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    serialNumbers.length > 0 && (
                        <Fragment>
                            <Grid item xs={3} className={classes.marginTop}>
                                <Dropdown
                                    value={selectedSernosGroup || ''}
                                    label="Filter by Sernos Group"
                                    fullWidth
                                    items={sernosGroups.length ? sernosGroups : ['']}
                                    onChange={handleFieldChange}
                                    propertyName="selectedSernosGroup"
                                />
                            </Grid>
                            <Grid item xs={3} className={classes.marginTop}>
                                <InputField
                                    label="Article Number"
                                    fullWidth
                                    type="string"
                                    onChange={handleFieldChange}
                                    propertyName="articleNumber"
                                    value={articleNumber}
                                />
                            </Grid>
                            <Grid item xs={6} />
                            <Grid item xs={1} />
                            <Grid item xs={3} className={classes.marginTop}>
                                <Button
                                    onClick={handlePrintMacAddressButtonClick}
                                    variant="outlined"
                                    color="secondary"
                                >
                                    Print MAC Labels
                                </Button>
                            </Grid>
                            <Grid item xs={3} className={classes.marginTop}>
                                <Button
                                    onClick={handlePrintAllButtonClick}
                                    variant="outlined"
                                    color="secondary"
                                >
                                    Print All Labels
                                </Button>
                            </Grid>
                            <Grid item xs={5} />
                        </Fragment>
                    )
                )}
                {!serialNumbersLoading && searchTerm && !serialNumbers.length && (
                    <Typography>{`Serial number ${searchTerm} not found`}</Typography>
                )}
            </Grid>
        </Page>
    );
}

LabelReprint.propTypes = {
    itemErrors: PropTypes.shape({}),
    fetchSerialNumbers: PropTypes.func.isRequired,
    serialNumbers: PropTypes.shape({}),
    serialNumbersLoading: PropTypes.bool
};

LabelReprint.defaultProps = {
    itemErrors: null,
    serialNumbers: null,
    serialNumbersLoading: false
};

export default LabelReprint;
