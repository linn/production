import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    SaveBackCancelButtons,
    TableWithInlineEditing,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AteTest({
    editStatus,
    itemError,
    history,
    profile,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible
}) {
    const [ateTest, setAteTest] = useState({});
    const [prevAteTest, setPrevAteTest] = useState({});

    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setAteTest(a => ({
                ...a,
                userNumber: profile.employee.replace('/employees/', ''), // the current user
                userName: profile.name
            }));
        }
    }, [profile, editStatus]);

    const tableColumns = [
        {
            title: 'No.',
            key: 'itemNumber',
            type: 'number'
        },
        {
            title: 'Board Fail No.',
            key: 'boardFailNumber',
            type: 'number'
        },
        {
            title: 'No. Fails',
            key: 'numberOfFails',
            type: 'number'
        },
        {
            title: 'Circuit Ref',
            key: 'circuitRef',
            type: 'text'
        },
        {
            title: 'Part',
            key: 'partNumber',
            type: 'text'
        },
        {
            title: 'Fault Code',
            key: 'ateTestFaultCode',
            type: 'text'
            // type: 'dropdown',
            // options: ateFaultCodes
        },
        {
            title: 'Smt or PCB?',
            key: 'smtOrPcb',
            type: 'text'
            // type: 'dropdownn'
            // options: []
        },
        {
            title: 'Shift',
            key: 'shift',
            type: 'text'
        },
        {
            title: 'Batch',
            key: 'batchNumber',
            type: 'number'
        },
        {
            title: 'AOI Escape',
            key: 'aoiEscape',
            type: 'text'
            // type: 'dropdown'
            // options: []
        },
        {
            title: 'PCB Operator',
            key: 'pcbOperator',
            type: 'text'
            // todo - name dropdown
            // type: 'dropdown'
            // options: []
        },
        {
            title: 'Comments',
            key: 'comments',
            type: 'text'
        },
        {
            title: 'Corrective Action',
            key: 'correctiveAction',
            type: 'text'
        },
        {
            title: 'Board SN',
            key: 'boardSerialNumber',
            type: 'text'
        }
    ];
    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevAteTest) {
            setAteTest(item);
            setPrevAteTest(item);
        }
    }, [item, prevAteTest]);

    const faultCodeInvalid = () => !ateTest.faultCode;
    const descriptionInvalid = () => !ateTest.description;

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, ateTest);
            setEditStatus('view');
        } else if (creating()) {
            addItem(ateTest);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setAteTest(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        // history.push('');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAteTest({ ...ateTest, [propertyName]: newValue });
    };

    const handleDetailFieldChange = (propertyName, newValue) => {
        setAteTest({ ...ateTest, [propertyName]: newValue });
        if (viewing()) {
            setEditStatus('edit');
        }
    };

    const updateOp = details => {
        handleDetailFieldChange('details', details);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Log ATE Test Results" />
                    ) : (
                        <Title text="ATE Test Results" />
                    )}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    ateTest &&
                    itemError?.faultCode !== 404 && (
                        <Fragment>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {!creating() ? (
                                <Fragment>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            value={ateTest.testId}
                                            label="Id"
                                            disabled
                                            onChange={handleFieldChange}
                                            propertyName="testId"
                                        />
                                    </Grid>
                                    <Grid item xs={10} />{' '}
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}
                            <Grid item xs={12}>
                                <Typography variant="h5">Details</Typography>
                            </Grid>
                            <Grid item xs={12}>
                                <TableWithInlineEditing
                                    columnsInfo={tableColumns}
                                    content={ateTest.details?.map(o => ({
                                        ...o,
                                        id: o.itemNumber
                                    }))}
                                    updateContent={updateOp}
                                    editStatus={editStatus}
                                    allowedToEdit={false}
                                    allowedToCreate={false}
                                    allowedToDelete={false}
                                />
                            </Grid>
                        </Fragment>
                    )
                )}
            </Grid>
        </Page>
    );
}

AteTest.propTypes = {
    item: PropTypes.shape({
        ateTest: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

AteTest.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default AteTest;
