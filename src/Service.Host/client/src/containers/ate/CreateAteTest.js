import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteTest from '../../components/ate/AteTest';
import ateTestActions from '../../actions/ateTestActions';
import ateTestSelectors from '../../selectors/ateTestSelectors';
import * as itemTypes from '../../itemTypes';
import getProfile from '../../selectors/userSelectors';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';
import worksOrdersActions from '../../actions/worksOrdersActions';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import ateFaultCodesSelectors from '../../selectors/ateFaultCodesSelectors';
import ateFaultCodeActions from '../../actions/ateFaultCodesActions';

const mapStateToProps = state => ({
    profile: getProfile(state),
    employees: employeesSelectors.getItems(state),
    employeesLoading: employeesSelectors.getLoading(state),
    editStatus: 'create',
    loading: ateTestSelectors.getLoading(state),
    snackbarVisible: ateTestSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.ateTest.item),
    worksOrdersSearchResults: worksOrdersSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.orderNumber, name: s.orderNumber })),
    worksOrdersSearchLoading: worksOrdersSelectors.getSearchLoading(state),
    ateFaultCodes: ateFaultCodesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
    dispatch(ateFaultCodeActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: ateTestActions.add,
    setEditStatus: ateTestActions.setEditStatus,
    setSnackbarVisible: ateTestActions.setSnackbarVisible,
    searchWorksOrders: worksOrdersActions.search,
    clearWorksOrdersSearch: worksOrdersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(AteTest));
