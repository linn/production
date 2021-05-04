import { connect } from 'react-redux';
import {
    getItemErrors,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PartFail from '../../components/partFails/PartFail';
import partFailActions from '../../actions/partFailActions';
import partFailSelectors from '../../selectors/partFailSelectors';
import partFailErrorTypesSelectors from '../../selectors/partFailErrorTypesSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import partFailErrorTypesActions from '../../actions/partFailErrorTypesActions';
import partFailFaultCodesSelectors from '../../selectors/partFailFaultCodesSelectors';
import partFailFailFaultCodesActions from '../../actions/partFailFailFaultCodesActions';
import getProfile from '../../selectors/userSelectors';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';
import worksOrdersActions from '../../actions/worksOrdersActions';
import purchaseOrdersSelectors from '../../selectors/purchaseOrdersSelectors';
import purchaseOrdersActions from '../../actions/purchaseOrdersActions';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';

const mapStateToProps = state => ({
    item: { details: [] },
    editStatus: 'create',
    errorTypes: partFailErrorTypesSelectors.getItems(state),
    faultCodes: partFailFaultCodesSelectors.getItems(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state, 100)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    storagePlaces: storagePlacesSelectors.getItems(state),
    loading: partFailSelectors.getLoading(state),
    snackbarVisible: partFailSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    profile: getProfile(state),
    worksOrdersSearchResults: worksOrdersSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.orderNumber, name: s.orderNumber })),
    worksOrdersSearchLoading: worksOrdersSelectors.getSearchLoading(state),
    purchaseOrdersSearchResults: purchaseOrdersSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.orderNumber, name: s.orderNumber })),
    purchaseOrdersSearchLoading: purchaseOrdersSelectors.getSearchLoading(state),
    requestErrors: getRequestErrors(state),
    errorTypesLoading: partFailErrorTypesSelectors.getLoading(state),
    faultCodesLoading: partFailFaultCodesSelectors.getLoading(state),
    storagePlacesSearchResults: storagePlacesSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.storagePlaceId, name: s.siteCode })),
    storagePlacesSearchLoading: storagePlacesSelectors.getSearchLoading(state),
    employees: employeesSelectors
        .getItems(state)
        .map(e => ({ ...e, name: e.id, description: e.fullName })),
    employeesLoading: employeesSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(partFailErrorTypesActions.fetch());
    dispatch(partFailFailFaultCodesActions.fetch());
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partFailActions.add,
    setEditStatus: partFailActions.setEditStatus,
    setSnackbarVisible: partFailActions.setSnackbarVisible,
    searchWorksOrders: worksOrdersActions.search,
    searchParts: partsActions.search,
    searchPurchaseOrders: purchaseOrdersActions.search,
    clearPartsSearch: partsActions.clearSearch,
    clearPurchaseOrdersSearch: purchaseOrdersActions.clearSearch,
    clearWorksOrdersSearch: worksOrdersActions.clearSearch,
    searchStoragePlaces: storagePlacesActions.search,
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    clearPartFailErrors: partFailActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartFail));
