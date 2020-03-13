import { connect } from 'react-redux';
import {
    getItemErrors,
    getItemErrorDetailMessage,
    initialiseOnMount,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import WorksOrder from '../../components/worksOrders/WorksOrder';
import worksOrderSelectors from '../../selectors/worksOrderSelectors';
import worksOrderActions, {
    getDefaultWorksOrderPrinter,
    setDefaultWorksOrderPrinter
} from '../../actions/worksOrderActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import employeesActions from '../../actions/employeesActions';
import worksOrderDetailsActions from '../../actions/worksOrderDetailsActions';
import worksOrderDetailsSelectors from '../../selectors/worksOrderDetailsSelectors';
import printWorksOrderLabelActions from '../../actions/printWorksOrderLabelsActions';
import printWorksOrderAioLabelActions from '../../actions/printWorksOrderAioLabelsActions';
import printWorksOrderLabelsSelectors from '../../selectors/printWorksOrderLabelsSelectors';
import printWorksOrderAioLabelsSelectors from '../../selectors/printWorksOrderAioLabelsSelectors';
import getWorksOrderDefaultPrinter from '../../selectors/localStorageSelectors';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';
import * as itemTypes from '../../itemTypes';
import * as processTypes from '../../processTypes';
import getProfile from '../../selectors/userSelectors';
import partsActions from '../../actions/partsActions';

const mapStateToProps = (state, { match }) => ({
    item: worksOrderSelectors.getItem(state),
    itemErrors: getItemErrors(state),
    orderNumber: match.params.id,
    profile: getProfile(state),
    worksOrderError: getItemErrorDetailMessage(state, itemTypes.worksOrder.item),
    worksOrderDetailsError: getItemErrorDetailMessage(state, itemTypes.worksOrderDetails.item),
    editStatus: worksOrderSelectors.getEditStatus(state),
    loading: worksOrderSelectors.getLoading(state),
    snackbarVisible: worksOrderSelectors.getSnackbarVisible(state),
    employees: employeesSelectors.getItems(state),
    employeeesLoading: employeesSelectors.getLoading(state),
    worksOrderDetails: worksOrderDetailsSelectors.getItem(state),
    printWorksOrderLabelsErrorDetail: getItemErrorDetailMessage(
        state,
        processTypes.printWorksOrderLabels.item
    ),
    printWorksOrderLabelsMessageVisible: printWorksOrderLabelsSelectors.getMessageVisible(state),
    printWorksOrderLabelsMessageText: printWorksOrderLabelsSelectors.getMessageText(state),
    printWorksOrderAioLabelsErrorDetail: getItemErrorDetailMessage(
        state,
        processTypes.printWorksOrderAioLabels.item
    ),
    printWorksOrderAioLabelsMessageVisible: printWorksOrderAioLabelsSelectors.getMessageVisible(
        state
    ),
    printWorksOrderAioLabelsMessageText: printWorksOrderAioLabelsSelectors.getMessageText(state),
    defaultWorksOrderPrinter: getWorksOrderDefaultPrinter(state),
    serialNumbers: serialNumberSelectors.getItems(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ orderNumber }) => dispatch => {
    if (orderNumber) {
        dispatch(worksOrderActions.fetch(orderNumber));
    }

    dispatch(employeesActions.fetch());
    dispatch(worksOrderDetailsActions.reset());
    dispatch(getDefaultWorksOrderPrinter());
};

const mapDispatchToProps = {
    initialise,
    setSnackbarVisible: worksOrderActions.setSnackbarVisible,
    fetchWorksOrderDetails: worksOrderDetailsActions.fetch,
    addItem: worksOrderActions.add,
    updateItem: worksOrderActions.update,
    setEditStatus: worksOrderActions.setEditStatus,
    fetchWorksOrder: worksOrderActions.fetch,
    printWorksOrderLabels: printWorksOrderLabelActions.requestProcessStart,
    clearPrintWorksOrderLabelsErrors: printWorksOrderLabelActions.clearErrorsForItem,
    setPrintWorksOrderLabelsMessageVisible: printWorksOrderLabelActions.setMessageVisible,
    printWorksOrderAioLabels: printWorksOrderAioLabelActions.requestProcessStart,
    clearPrintWorksOrderAioLabelsErrors: printWorksOrderAioLabelActions.clearErrorsForItem,
    setPrintWorksOrderAioLabelsMessageVisible: printWorksOrderAioLabelActions.setMessageVisible,
    clearErrors: worksOrderActions.clearErrorsForItem,
    setDefaultWorksOrderPrinter,
    fetchSerialNumbers: serialNumberActions.fetchByQueryString,
    searchParts: partsActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(WorksOrder));
