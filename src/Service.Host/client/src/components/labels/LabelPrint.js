import React, { useState, useCallback, useEffect, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    InputField,
    Loading,
    SearchInputField,
    Title,
    ErrorCard,
    SnackbarMessage,
    useSearch,
    utilities
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

function LabelPrint({
    printMACLabelsErrorDetail,
    serialNumberErrorDetail,
    fetchSerialNumbers,
    serialNumbers,
    serialNumbersLoading,
    printAllLabelsForProductMessageVisible,
    printAllLabelsForProductMessageText,
    printMACLabelsMessageVisible,
    printMACLabelsMessageText,
    setPrintAllLabelsForProductActionsMessageVisible,
    printAllLabelsForProduct,
    setPrintMACLabelsActionsMessageVisible,
    printMACLabels,
    printAllLabelsForProductErrorDetail,
    clearMacLabelErrors,
    clearAllLabelErrors
}) {
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

            clearMacLabelErrors();
            clearAllLabelErrors();
            setSernosGroups(sortedGroups);
            setSelectedSernosGroup(sortedGroups[0] || '');
            selectSerialNumber(sortedGroups[0]);
        }
    }, [serialNumbers, selectSerialNumber, clearMacLabelErrors, clearAllLabelErrors]);

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

    const handlePrintMacAddressButtonClick = () => {
        clearMacLabelErrors();
        printMACLabels({ serialNumber: searchTerm });
    };

    const handlePrintAllButtonClick = () => {
        clearAllLabelErrors();
        printAllLabelsForProduct({ serialNumber: searchTerm, articleNumber });
    };

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Reprint Product Labels" />
                </Grid>
                <SnackbarMessage
                    visible={printMACLabelsMessageVisible}
                    onClose={() => setPrintMACLabelsActionsMessageVisible(false)}
                    message={printMACLabelsMessageText}
                />
                <SnackbarMessage
                    visible={printAllLabelsForProductMessageVisible}
                    onClose={() => setPrintAllLabelsForProductActionsMessageVisible(false)}
                    message={printAllLabelsForProductMessageText}
                />
                <Grid item xs={12}>
                    {printMACLabelsErrorDetail && (
                        <ErrorCard errorMessage={printMACLabelsErrorDetail} />
                    )}
                    {serialNumberErrorDetail && (
                        <ErrorCard errorMessage={serialNumberErrorDetail} />
                    )}
                    {printAllLabelsForProductErrorDetail && (
                        <ErrorCard errorMessage={printAllLabelsForProductErrorDetail} />
                    )}
                </Grid>
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
                                    allowNoValue
                                    items={sernosGroups}
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

LabelPrint.propTypes = {
    fetchSerialNumbers: PropTypes.func.isRequired,
    serialNumbers: PropTypes.arrayOf(PropTypes.shape({})),
    serialNumbersLoading: PropTypes.bool,
    setPrintAllLabelsForProductActionsMessageVisible: PropTypes.func.isRequired,
    setPrintMACLabelsActionsMessageVisible: PropTypes.func.isRequired,
    printAllLabelsForProduct: PropTypes.func.isRequired,
    printMACLabels: PropTypes.func.isRequired,
    clearMacLabelErrors: PropTypes.func.isRequired,
    clearAllLabelErrors: PropTypes.func.isRequired,
    printMACLabelsMessageVisible: PropTypes.bool,
    printAllLabelsForProductMessageVisible: PropTypes.bool,
    printAllLabelsForProductMessageText: PropTypes.string,
    printMACLabelsMessageText: PropTypes.string,
    printMACLabelsErrorDetail: PropTypes.string,
    printAllLabelsForProductErrorDetail: PropTypes.string,
    serialNumberErrorDetail: PropTypes.string
};

LabelPrint.defaultProps = {
    serialNumbers: null,
    serialNumbersLoading: false,
    printAllLabelsForProductMessageVisible: false,
    printMACLabelsMessageVisible: false,
    printAllLabelsForProductMessageText: '',
    printMACLabelsMessageText: '',
    printMACLabelsErrorDetail: '',
    printAllLabelsForProductErrorDetail: '',
    serialNumberErrorDetail: ''
};

export default LabelPrint;
