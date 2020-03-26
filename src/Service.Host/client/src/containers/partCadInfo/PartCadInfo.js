import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import partCadInfoActions from '../../actions/partCadInfoActions';
import partCadInfoSelectors from '../../selectors/partCadInfoSelectors';
import partCadInfosActions from '../../actions/partCadInfosActions';
import partCadInfosSelectors from '../../selectors/partCadInfosSelectors';
import PartCadInfo from '../../components/partCadInfo/PartCadInfo';

const mapStateToProps = state => ({
    loading: partCadInfoSelectors.getLoading(state),
    item: partCadInfoSelectors.getItem(state),
    snackbarVisible: partCadInfoSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    editStatus: partCadInfoSelectors.getEditStatus(state),
    partCadInfosSearchResults: partCadInfosSelectors
        .getSearchItems(state)
        .map(p => ({ ...p, id: p.msId, name: p.partNumber })),
    partCadInfosSearchLoading: partCadInfosSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchPartCadInfos: partCadInfosActions.search,
    clearPartCadInfosSearch: partCadInfosActions.clearSearch,
    updatePartCadInfo: partCadInfoActions.update,
    setSnackbarVisible: partCadInfoActions.setSnackbarVisible,
    setEditStatus: partCadInfoActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(PartCadInfo);
