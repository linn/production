import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import partCadInfoActions from '../../actions/partCadInfoActions';
import partCadInfoSelectors from '../../selectors/partCadInfoSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import PartCadInfo from '../../components/partCadInfo/PartCadInfo';

const mapStateToProps = state => ({
    loading: partCadInfoSelectors.getLoading(state),
    item: partCadInfoSelectors.getItem(state),
    snackbarVisible: partCadInfoSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    editStatus: partCadInfoSelectors.getEditStatus(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(p => ({ ...p, id: p.partNumber, name: p.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    updatePartCadInfo: partCadInfoActions.update,
    setSnackbarVisible: partCadInfoActions.setSnackbarVisible,
    setEditStatus: partCadInfoActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(PartCadInfo);
